namespace IdleGame.Api.Contracts
{
    public class PlayerAchievementsDto
    {
        public int Id { get; set; }
        public string TrainingName { get; set; }
        public int RequiredCount { get; set; }
        public int Reward { get; set; }
        public bool Achieved { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }
}
