namespace IdleGame.Domain.Entities
{
    public class AchievementsEntity
    {
        public int Id { get; set; }
        public string TrainingName { get; set; }
        public int RequiredCount { get; set; }
        public int Reward { get; set; }
        public string Description { get; set; }
    }
}
