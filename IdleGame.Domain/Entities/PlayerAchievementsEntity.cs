namespace IdleGame.Domain.Entities
{
    public class PlayerAchievementsEntity
    {
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public bool Achieved { get; set; }
        public AchievementsEntity Achievement { get; set; }
    }
}
