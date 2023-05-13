namespace IdleGame.Domain.Entities
{
    public class PlayerIdleTrainingEntity
    {
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public IdleTrainingEntity IdleTraining { get; set; }
        public DateTime StartTime { get; set; }
        public bool Active { get; set; }
    }
}
