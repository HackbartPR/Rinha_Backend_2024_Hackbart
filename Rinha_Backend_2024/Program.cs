using Microsoft.EntityFrameworkCore;
using Rinha_Backend_2024.Database;
using Rinha_Backend_2024.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.ToListAsync());

app.Run();
