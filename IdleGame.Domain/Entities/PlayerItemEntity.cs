namespace IdleGame.Domain.Entities
{
    public class PlayerItemEntity
    {
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public int Amount { get; set; }
        public bool IsEquiped { get; set; }
        public ItemEntity Item { get; set; }
    }
}
