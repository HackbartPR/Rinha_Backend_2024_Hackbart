using Microsoft.EntityFrameworkCore;
using Rinha_Backend_2024.Database;
using Rinha_Backend_2024.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Contexto>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/clientes", async (Contexto db) =>
    await db.Clientes.ToListAsync());

app.MapGet("/transacoes", async (Contexto db) =>
    await db.Transacoes.ToListAsync());

app.MapPost("/clientes/{id}/transacoes", async (int id, Transacao transacao, Contexto db, CancellationToken cancellationToken) =>
{
    Cliente? cliente = await db.Clientes.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    if (cliente == null) return Results.NotFound();

    if (!cliente.Transacao(transacao))
        return Results.StatusCode(422);

    transacao.Registrar(cliente.Id);

    db.Add(transacao);
    await db.SaveChangesAsync(cancellationToken);

    return Results.Ok(new { cliente.Limite, cliente.Saldo});
});

app.MapGet("/clientes/{id}/extrato", async (int id, Contexto db, CancellationToken cancellationToken) =>
{
    if(!await db.Clientes.AnyAsync(cancellationToken))
        return Results.NotFound();

    return Results.Ok(
        await db.Clientes
        .Where(c => c.Id == id)
        .Select(c => new
        {
            Saldo = new
            {
                Total = c.Saldo,
                DataExtrato = DateTime.UtcNow,
                c.Limite
            },
            Ultimas_Transacoes = c.Transacoes
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
