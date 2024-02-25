using System.ComponentModel.DataAnnotations.Schema;

namespace Rinha_Backend_2024.Models
{
    //[Table("clientes")]
    public class Cliente
    {
        //[Column("id")]
        public int Id { get; set; }
        
        //[Column("limite")]
        public long Limite {  get; set; }

        //[Column("saldo")]
        public long Saldo { get; set; }

        public virtual ICollection<Transacao> Transacoes { get; set; } = new HashSet<Transacao>();
    }
}
