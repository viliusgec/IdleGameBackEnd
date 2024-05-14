namespace IdleGame.Domain.Entities
{
    public class PlayerStatisticsEntity
    {
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public string TrainingName { get; set; }
        public int Count { get; set; }
    }
}
