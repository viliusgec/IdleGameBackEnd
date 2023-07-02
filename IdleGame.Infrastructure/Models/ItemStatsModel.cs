using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("ItemStats", Schema = "Clicker")]
    public class ItemStatsModel
    {
        [Key]
        public int Id { get; set; }
        public string Item { get; set; }
        public int? HP { get; set; }
        public int? Defense { get; set; }
        public int? Attack { get; set; }
    }
}
