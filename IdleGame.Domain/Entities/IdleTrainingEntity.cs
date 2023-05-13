namespace IdleGame.Domain.Entities
{
    public class IdleTrainingEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SkillName { get; set; }
        public int XpGiven { get; set; }
        public int XpNeeded { get; set; }
    }
}
