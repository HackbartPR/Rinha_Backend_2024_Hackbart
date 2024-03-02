using Rinha_Backend_2024.Domain.Entities.Shared;

namespace Rinha_Backend_2024.Domain.Entities.Extrato
{
    public class ExtratoSaldo : Saldo
    {
        public DateTime Data_Extrato {  get; private set; }

        public ExtratoSaldo(DateTime data_Extrato, long limite, long valor): base(limite, valor)
        {
            Data_Extrato = data_Extrato;
        }
    }
}
