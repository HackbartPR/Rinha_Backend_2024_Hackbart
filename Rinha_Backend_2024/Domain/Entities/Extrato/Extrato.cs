using Rinha_Backend_2024.Domain.Entities.Shared;

namespace Rinha_Backend_2024.Domain.Entities.Extrato
{
    public class Extrato
    {
        public ExtratoSaldo Saldo { get; private set; }

        public ICollection<Transacao> Ultimas_Transacoes {  get; set; }

        public Extrato (ExtratoSaldo saldo, ICollection<Transacao> ultimas_Transacoes)
        {
            Saldo = saldo;
            Ultimas_Transacoes = ultimas_Transacoes;
        }
    }
}
