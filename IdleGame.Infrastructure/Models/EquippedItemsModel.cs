using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("EquippedItems", Schema = "Clicker")]
    public class EquippedItemsModel
    {
        [Key]
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public string? Item { get; set; }

        // Head, body, legs, feet, tool
        public string ItemPlace { get; set; }
    }
}
