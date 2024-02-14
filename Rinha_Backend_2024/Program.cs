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

app.MapPost("/clientes/{id}/transacoes", async (int id, Transacao transacao, Contexto db) =>
{
    Cliente? cliente = await db.Clientes.FirstOrDefaultAsync(c => c.Id == id);
    if (cliente == null) return Results.NotFound();

    if (transacao.Tipo.Equals('d'))
    {
        long valor = cliente.Saldo - transacao.Valor;
        if (valor < cliente.Limite) return Results.StatusCode(422);

        cliente.Saldo = valor;
    }
    else if (transacao.Tipo.Equals('c'))
        cliente.Saldo += transacao.Valor;

    transacao.ClienteId = id;
    transacao.DataCriacao = DateTime.UtcNow;

    db.Add(transacao);
    await db.SaveChangesAsync();

    // Utilizar procedure para fazer este papel acima? 

    return Results.Ok(new { cliente.Limite, cliente.Saldo});
});

app.Run();
