using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("PlayerStatistics", Schema = "Clicker")]
    public class PlayerStatisticsModel
    {
        [Key]
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public string TrainingName { get; set; }
        public int Count { get; set; }
    }
}
