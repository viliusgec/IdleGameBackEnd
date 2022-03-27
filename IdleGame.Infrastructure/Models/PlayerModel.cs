using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("Players", Schema = "Clicker")]
    public class PlayerModel
    {
        [Key]
        public string Username { get; set; }
        public int Money { get; set; }
        public bool IsInBattle { get; set; }
        public bool IsInAction { get; set; }
        public int InventorySpace { get; set; }
    }
}
