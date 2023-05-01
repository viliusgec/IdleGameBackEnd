using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("Achievements", Schema = "Clicker")]
    public class AchievementsModel
    {
        [Key]
        public int Id { get; set; }
        public string SkillType { get; set; }
        public int RequiredXP { get; set; }
        public int Reward { get; set; }
        public string Description { get; set; }
    }
}
