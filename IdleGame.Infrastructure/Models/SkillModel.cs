using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("Skills", Schema = "Clicker")]
    public class SkillModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public string PlayerUsername { get; set; }
    }
}
