using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("PlayerItems", Schema = "Clicker")]
    public class PlayerItemModel
    {
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public bool IsEquiped { get; set; }
        public int Amount { get; set; }
        public string ItemName { get; set; }
    }
}
