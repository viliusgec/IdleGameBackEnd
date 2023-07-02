namespace IdleGame.Domain.Entities
{
    public class EquippedItemsEntity
    {
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public ItemEntity? Item { get; set; }

        // Head, body, legs, feet, tool
        public string ItemPlace { get; set; }
    }
}
