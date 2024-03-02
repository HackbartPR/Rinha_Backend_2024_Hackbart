using Rinha_Backend_2024.Domain.Config.Exceptions;
using Rinha_Backend_2024.Domain.Entities.Shared;

namespace Rinha_Backend_2024.Domain.Entities.Cliente
{
    public class Cliente
    {
        public int Id { get; private set; }

        public Saldo Saldo { get; private set; }

        public Cliente(int id, Saldo saldo) 
        {
            Id = id;
            Saldo = saldo;

            Validate();
        }

        private void Validate()
        {
            if (Id < 1 || Id > 5) throw new NotFoundException();
        }
    }
}
