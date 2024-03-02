using Microsoft.AspNetCore.Mvc;
using Rinha_Backend_2024.Domain.Config.Exceptions;
using Rinha_Backend_2024.Domain.Entities.Cliente;
using Rinha_Backend_2024.Domain.Entities.Shared;
using Rinha_Backend_2024.Domain.Repositories.Cliente;
using Rinha_Backend_2024.Domain.Repositories.Extrato;
using Rinha_Backend_2024.Domain.Services.Cliente;
using Rinha_Backend_2024.Infrastructure.Repositories.Cliente;
using Rinha_Backend_2024.Infrastructure.Repositories.Extrato;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IExtratoRepository, ExtratoRepository>();
builder.Services.AddSingleton<IConfigurationRoot>(builder.Configuration);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/clientes/{id}/transacoes", async ([FromRoute] int id, [FromBody] Transacao transacao, IClienteRepository repository, CancellationToken cancellationToken) =>
{
    try
    {
        Cliente cliente = await repository.ConsultarAsync(id);
        ClienteTransacao clienteTransacao = new(null, cliente.Id, transacao);
        ClienteService.Operacionar(cliente, clienteTransacao);
        
        await repository.TransacionarAsync(cliente, clienteTransacao);
        return Results.Ok(cliente.Saldo);
    }
    catch (NotFoundException) { return Results.NotFound(); }
    catch (UnprocessableEntityException) { return Results.UnprocessableEntity(); }
    catch (Exception ex) { return Results.Problem(ex.Message); }
});

app.MapGet("/clientes/{id}/extrato", async ([FromRoute] int id, IClienteRepository clienteRepository, IExtratoRepository extratoRepository, CancellationToken cancellationToken) =>
{
    try
    {
        Cliente cliente = await clienteRepository.ConsultarAsync(id);
        return Results.Ok(await extratoRepository.ConsultarAsync(cliente));
    }
    catch (NotFoundException) { return Results.NotFound(); }
    catch (UnprocessableEntityException) { return Results.UnprocessableEntity(); }
    catch (Exception ex) { return Results.Problem(ex.Message); }
});

app.Run();
