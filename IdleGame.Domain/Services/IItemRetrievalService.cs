using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Services
{
    public interface IItemRetrievalService
    {
        public Task<IEnumerable<ItemEntity>> GetItems();
        public Task<IEnumerable<ItemEntity>> GetShopItems();
        public Task<IEnumerable<MarketItemEntity>> GetMarketItems();
        public Task<IEnumerable<MarketItemEntity>> GetPlayerMarketItems(string username);
        public Task<MarketItemEntity> PostMarketItem(MarketItemEntity item);
        public MarketItemEntity PutMarketItem(MarketItemEntity item);
        public MarketItemEntity DeleteMarketItem(MarketItemEntity item);
        public Task<IEnumerable<PlayerItemEntity>> GetPlayerItems(string username);
        public Task<PlayerItemEntity> GetPlayerItem(string username, string itemName);
        public Task<PlayerItemEntity> PostPlayerItem(PlayerItemEntity playerItem);
        public PlayerItemEntity PutPlayerItem(PlayerItemEntity playerItem);
        public Task<IEnumerable<EquippedItemsEntity>> GetPlayerEquippedItems(string username);
        public Task<EquippedItemsEntity> GetPlayerEquippedItem(string username, string itemType);
        public EquippedItemsEntity PutEquippedItem(EquippedItemsEntity item);
        public Task<EquippedItemsEntity> PostEquippedItem(EquippedItemsEntity item);
    }
}
