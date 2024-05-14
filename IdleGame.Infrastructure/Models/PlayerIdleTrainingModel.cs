using System.ComponentModel.DataAnnotations.Schema;

namespace IdleGame.Infrastructure.Models
{
    [Table("PlayerIdleTrainings", Schema = "Clicker")]
    public class PlayerIdleTrainingModel
    {
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public int IdleTrainingId { get; set; }
        public DateTime StartTime { get; set; }
        public bool Active { get; set; }
    }
}
