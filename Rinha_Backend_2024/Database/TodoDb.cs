using Microsoft.EntityFrameworkCore;
using Rinha_Backend_2024.Models;

namespace Rinha_Backend_2024.Database
{
    class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options)
            : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();
    }
}
