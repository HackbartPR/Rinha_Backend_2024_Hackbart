namespace Rinha_Backend_2024.Infrastructure.Models.Cliente
{
    public class ClienteTransacaoModel
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public long Valor { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public DateTime Realizada_Em { get; set; }
    }
}
