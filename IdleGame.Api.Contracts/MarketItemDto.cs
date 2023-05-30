namespace IdleGame.Api.Contracts
{
    public class MarketItemDto
    {
        public int? Id { get; set; }
        public string ItemName { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public string? Player { get; set; }
    }
}
