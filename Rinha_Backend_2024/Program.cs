using Microsoft.EntityFrameworkCore;
using Rinha_Backend_2024.Database;
using Rinha_Backend_2024.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Contexto>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/clientes/{id}/transacoes", async (int id, Transacao transacao, Contexto db, CancellationToken cancellationToken) =>
{
    Cliente? cliente = await db.Clientes
        .FromSql($"SELECT * FROM \"Clientes\" FOR SHARE")
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync(cancellationToken);

    if (cliente == null) return Results.NotFound();

    if (transacao.Valor < 0)
        return Results.UnprocessableEntity();

    if (!transacao.Tipo.Equals("d") && !transacao.Tipo.Equals("c"))
        return Results.UnprocessableEntity();

    if (transacao.Descricao.Length < 1 && transacao.Descricao.Length > 10)
        return Results.UnprocessableEntity();

    if (transacao.Tipo.Equals("c"))
        cliente.Saldo += transacao.Valor;

    if (transacao.Tipo.Equals("d"))
    {
        if (cliente.Saldo - transacao.Valor < cliente.Limite)
            return Results.UnprocessableEntity();

        cliente.Saldo -= transacao.Valor;
    }

    transacao.Cliente = cliente;
    transacao.DataCriacao = DateTime.UtcNow;

    await db.AddAsync(transacao, cancellationToken);
    await db.SaveChangesAsync(cancellationToken);

    return Results.Ok(new { cliente.Limite, cliente.Saldo});
});

app.MapGet("/clientes/{id}/extrato", async (int id, Contexto db, CancellationToken cancellationToken) =>
{
    if(!await db.Clientes.AnyAsync(c => c.Id == id, cancellationToken))
        return Results.NotFound();

    return Results.Ok(
        await db.Clientes
        .FromSql($"SELECT * FROM \"Clientes\" FOR SHARE")
        .AsNoTracking()
        .Where(c => c.Id == id)
        .Select(c => new
        {
            Saldo = new
            {
                Total = c.Saldo,
                DataExtrato = DateTime.UtcNow,
                c.Limite
            },
            Ultimas_Transacoes = db.Transacoes
                .FromSql($"SELECT * FROM \"Transacoes\" FOR SHARE")
                .AsNoTracking()
                .Where(t => t.ClienteId == id)
                .Select(t => new
                {
                    t.Valor,
                    t.Tipo,
                    t.Descricao,
                    Realizada_Em = t.DataCriacao
                })
                .Take(10).OrderByDescending(t => t.Realizada_Em).ToList(),
        })
        .FirstOrDefaultAsync(cancellationToken)
    ) ;
});

app.Run();
