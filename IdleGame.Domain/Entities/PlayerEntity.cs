namespace IdleGame.Domain.Entities
{
    public class PlayerEntity
    {
        public string Username { get; set; }
        public int Money { get; set; }
        public bool IsInBattle { get; set; }
        public bool IsInAction { get; set; }
        public int InventorySpace { get; set; }
    }
}
