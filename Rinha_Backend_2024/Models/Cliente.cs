namespace Rinha_Backend_2024.Models
{
    /// <summary>
    /// Representação do Modelo do Banco e da Entity Cliente
    /// </summary>
    public class Cliente
    {
        public int Id { get; set; }
        public long Limite {  get; set; }
        public long Saldo { get; set; }

        public virtual ICollection<Transacao> Transacoes { get; set; } = new HashSet<Transacao>();

        /// <summary>
        /// Método Principal para Organizar a Transação
        /// </summary>
        /// <param name="transacao"></param>
        /// <returns></returns>
        public bool Transacao(Transacao transacao)
        {
            if (!Validate(transacao))
                return false;

            return transacao.Tipo.Equals('d') ? Debitar(transacao.Valor) : Creditar(transacao.Valor);
        }

        /// <summary>
        /// Transação do Modo Crédito
        /// </summary>
        /// <param name="trsValor">Valor da Transação</param>
        /// <returns></returns>
        private bool Creditar(long trsValor)
        {
            Saldo += trsValor;
            return true;
        }

        /// <summary>
        /// Transação do Modo Débito
        /// </summary>
        /// <param name="trsValor">Valor da Transação</param>
        /// <returns></returns>
        private bool Debitar(long trsValor)
        {
            long valor = Saldo - trsValor;
            
            if (valor < Limite) 
                return false;

            Saldo = valor;
            return true;
        }

        /// <summary>
        /// Realiza a validação de acordo com a regra e negócio
        /// </summary>
        /// <param name="transacao"></param>
        /// <returns></returns>
        private static bool Validate(Transacao transacao)
        {
            if (transacao.Valor <= 0)
                return false;

            if(!transacao.Tipo.Equals('d') && !transacao.Tipo.Equals('c'))
                return false;

            return true;
        }

    }
}
