namespace Rinha_Backend_2024.Models
{
    /// <summary>
    /// Representação do Modelo do Banco e da Entity Transação
    /// </summary>
    public class Transacao
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public long Valor { get; set; }
        public char Tipo { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCriacao {  get; set; }

        public virtual Cliente Cliente { get; set; } = null!;

        /// <summary>
        /// Realiza a finalização da transação
        /// </summary>
        /// <param name="clientId"></param>
        public void Registrar(int clientId)
        {
            ClienteId = clientId;
            DataCriacao = DateTime.UtcNow;
        }
    }
}
