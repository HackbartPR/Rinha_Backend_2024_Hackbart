using Rinha_Backend_2024.Domain.Config.Exceptions;

namespace Rinha_Backend_2024.Domain.Entities.Shared
{
    public class Transacao
    {
        public long Valor { get; private set; }

        public string Tipo { get; private set; }

        public string Descricao { get; private set; }

        public DateTime Realizada_Em { get; private set; }

        public Transacao(long valor, string tipo, string descricao, DateTime realizada_em) 
        {
            Valor = valor;
            Tipo = tipo;
            Descricao = descricao;
            Realizada_Em = realizada_em;
        }
    }
}
