using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("PvP", Schema = "Clicker")]
    public class PvPModel
    {
        [Key]
        public int Id { get; set; }
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public int Bet { get; set; }
        public DateTime Date { get; set; }
        public string Winner { get; set; }
    }
}
