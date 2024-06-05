using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;

namespace IdleGame.Domain.Services
{
    public class ItemRetrievalService(IItemRepository itemRepository) : IItemRetrievalService
    {
        private readonly IItemRepository _itemRepository = itemRepository;

        public Task<IEnumerable<ItemEntity>> GetItems()
        {
            return _itemRepository.GetItems();
        }

        public Task<ItemEntity> GetItem(string name)
        {
            return _itemRepository.GetItem(name);
        }

        public ItemEntity UpdateItem(ItemEntity item)
        {
            return _itemRepository.PutItem(item);
        }

        public Task<IEnumerable<ItemEntity>> GetShopItems()
        {
            return _itemRepository.GetShopItems();
        }

        public Task<ItemEntity> PostShopItem(ItemEntity item)
        {
            return _itemRepository.PostShopItem(item);
        }

        public Task<IEnumerable<PlayerItemEntity>> GetPlayerItems(string username)
        {
            return _itemRepository.GetPlayerItems(username);
        }

        public Task<PlayerItemEntity> GetPlayerItem(string username, string itemName)
        {
            return _itemRepository.GetPlayerItem(username, itemName);
        }

        public Task<PlayerItemEntity> PostPlayerItem(PlayerItemEntity playerItem)
        {
            return _itemRepository.PostPlayerItem(playerItem);
        }

        public PlayerItemEntity PutPlayerItem(PlayerItemEntity playerItem)
        {
            return _itemRepository.PutPlayerItem(playerItem);
        }

        public Task<IEnumerable<MarketItemEntity>> GetMarketItems()
        {
            return _itemRepository.GetMarketItems();
        }

        public Task<IEnumerable<MarketItemEntity>> GetPlayerMarketItems(string username)
        {
            return _itemRepository.GetPlayerMarketItems(username);
        }

        public Task<MarketItemEntity> PostMarketItem(MarketItemEntity item)
        {
            return _itemRepository.PostMarketItem(item);
        }

        public MarketItemEntity PutMarketItem(MarketItemEntity item)
        {
            return _itemRepository.PutMarketItem(item);
        }

        public MarketItemEntity DeleteMarketItem(MarketItemEntity item)
        {
            return _itemRepository.DeleteMarketItem(item);
        }

        public Task<IEnumerable<EquippedItemsEntity>> GetPlayerEquippedItems(string username)
        {
            return _itemRepository.GetPlayerEquippedItems(username);
        }

        public Task<EquippedItemsEntity> GetPlayerEquippedItem(string username, string itemType)
        {
            return _itemRepository.GetPlayerEquippedItem(username, itemType);
        }
        public EquippedItemsEntity PutEquippedItem(EquippedItemsEntity item)
        {
            return _itemRepository.PutEquippedItem(item);
        }
        public Task<EquippedItemsEntity> PostEquippedItem(EquippedItemsEntity item)
        {
            return _itemRepository.PostEquippedItem(item);
        }

    }
}
