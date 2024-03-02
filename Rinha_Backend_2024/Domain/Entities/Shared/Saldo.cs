using Rinha_Backend_2024.Domain.Config.Exceptions;

namespace Rinha_Backend_2024.Domain.Entities.Shared
{
    public class Saldo
    {
        public long Limite {  get; private set; }

        public long Valor { get; private set; }

        public Saldo(long limite, long valor) 
        {
            Valor = valor;
            Limite = limite;
        }

        public void Creditar(long credito)
        {
            if (credito < 0 || credito % 1 != 0) throw new UnprocessableEntityException();

            Valor += credito;
        }

        public void Debitar(long debito)
        {
            if (debito < 0 || debito % 1 != 0) throw new UnprocessableEntityException();
            if (Valor - debito < Limite) throw new UnprocessableEntityException();

            Valor -= debito;
        }
    }
}
