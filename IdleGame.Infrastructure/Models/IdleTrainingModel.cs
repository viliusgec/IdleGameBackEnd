using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("IdleTrainings", Schema = "Clicker")]
    public class IdleTrainingModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SkillName { get; set; }
        public int XpGiven { get; set; }
        public int XpNeeded { get; set; }
    }
}
