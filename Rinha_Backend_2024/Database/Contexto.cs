using Microsoft.EntityFrameworkCore;
using Rinha_Backend_2024.Models;

namespace Rinha_Backend_2024.Database
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options) { }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Transacao> Transacoes => Set<Transacao>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasMany(cl => cl.Transacoes)
                .WithOne(tr => tr.Cliente)
                .HasForeignKey(cl => cl.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
