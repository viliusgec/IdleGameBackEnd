namespace IdleGame.Domain.Entities
{
    public class MonsterEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int Defense { get; set; }
        public int Attack { get; set; }
        public string DroppedItem { get; set; }
        public int ItemDropChance { get; set; }
        public int XpGiven { get; set; }
    }
}
