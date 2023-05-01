using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdleGame.Infrastructure.Models
{
    [Table("PlayerAchievements", Schema = "Clicker")]
    public class PlayerAchievementsModel
    {
        [Key]
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public int AchievementId { get; set; }
        public bool Achieved { get; set; }
    }
}
