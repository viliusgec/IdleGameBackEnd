namespace IdleGame.Domain.Entities
{
    public class ItemEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? SellPrice { get; set; }
        public bool isSellable { get; set; }
        public string Type { get; set; }
        public int? HP { get; set; }
        public int? Defense { get; set; }
        public int? Attack { get; set; }
    }
}
