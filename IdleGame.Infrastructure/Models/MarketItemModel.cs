using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("MarketItems", Schema = "Clicker")]
    public class MarketItemModel
    {
        public int Id { get; set; }
        public string Player { get; set; }
        public int Price { get; set; }
        public int Ammount { get; set; }
        public string ItemName { get; set; }
    }
}
