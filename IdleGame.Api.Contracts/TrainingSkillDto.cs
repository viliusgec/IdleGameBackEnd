namespace IdleGame.Api.Contracts
{
    public class TrainingSkillDto
    {
        public string TrainingName { get; set; }
        public int SkillLevelRequired { get; set; }
        public int XpGiven {  get; set; }
        public string? GivenItem { get; set; }
        public int? GivenItemAmount { get; set; }
        public string? NeededItem { get; set; }
        public int? NeededItemAmount { get; set; }
    }
}
