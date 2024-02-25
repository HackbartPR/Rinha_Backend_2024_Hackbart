using System.ComponentModel.DataAnnotations.Schema;

namespace Rinha_Backend_2024.Models
{
    //[Table("transacoes")]
    public class Transacao
    {
        //[Column("id")]
        public int Id { get; set; }

        //[Column("clienteid")]
        public int ClienteId { get; set; }

        //[Column("valor")]
        public long Valor { get; set; }

        //[Column("tipo")]
        public string Tipo { get; set; } = string.Empty;

        //[Column("descricao")]
        public string Descricao { get; set; } = string.Empty;

        ///[Column("datacriacao")]
        public DateTime DataCriacao {  get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
    }
}
