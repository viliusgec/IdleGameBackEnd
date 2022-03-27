using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("Items", Schema = "Clicker")]
    public class ItemModel
    {
        [Key]
        public string Name { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool isSellable { get; set; }
    }
}
