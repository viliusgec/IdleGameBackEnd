namespace IdleGame.Api.Contracts
{
    public class PlayerItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Level { get; set; }
        public int Ammount { get; set; }
        public bool IsEquiped { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
