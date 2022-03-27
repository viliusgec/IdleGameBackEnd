using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("TrainingSkills", Schema = "Clicker")]
    public class TrainingSkillModel
    {
        [Key]
        public string TrainingName { get; set; }
        public string SkillType { get; set; }
        public int SkillLevelRequired { get; set; }
        public int XpGiven { get; set; }
        public string? GivenItem { get; set; }
        public int? GivenItemAmount { get; set; }
        public string? NeededItem { get; set; }
        public int? NeededItemAmount { get; set; }
    }
}
