using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("ShopItems", Schema = "Clicker")]
    public class ShopItemsModel
    {
        [Key]
        public string ItemName { get; set; }
    }
}
