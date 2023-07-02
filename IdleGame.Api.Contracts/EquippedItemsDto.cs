namespace IdleGame.Api.Contracts
{
    public class EquippedItemsDto
    {
        public int Id { get; set; }
        public string PlayerUsername { get; set; }
        public ItemDto? Item { get; set; }

        // Head, body, legs, feet, tool
        public string ItemPlace { get; set; }
    }
}
