using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;

namespace IdleGame.ApplicationServices.Services
{
    public interface IItemService
    {
        public Task<IEnumerable<ItemEntity>> GetItems();
        public Task<IEnumerable<ItemEntity>> GetShopItems();
        public Task<IEnumerable<MarketItemEntity>> GetMarketItems();
        public Task<IEnumerable<MarketItemEntity>> GetPlayerMarketItems(string username);
        public Task<MarketItemEntity> SellMarketItems(MarketItemDto item);
        public Task<MarketItemEntity> BuyMarketItems(string username, MarketItemDto item);
        public Task<MarketItemEntity> CancelMarketListing(string username, MarketItemDto item);
        public Task<IEnumerable<PlayerItemEntity>> GetPlayerItems(string username);
        public Task<PlayerItemEntity> SellPlayerItems(string username, PlayerItemDto playerItem, int Amount);
        public Task<PlayerEntity> BuyItems(string username, string item, int Amount);
    }
}
