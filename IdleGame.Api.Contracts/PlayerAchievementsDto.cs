namespace IdleGame.Api.Contracts
{
    public class PlayerAchievementsDto
    {
        public int Id { get; set; }
        public string SkillType { get; set; }
        public int RequiredXP { get; set; }
        public int Reward { get; set; }
        public bool Achieved { get; set; }
        public string Description { get; set; }
    }
}
