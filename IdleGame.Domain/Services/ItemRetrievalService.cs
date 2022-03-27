using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;

namespace IdleGame.Domain.Services
{
    public class ItemRetrievalService : IItemRetrievalService
    {
        private readonly IItemRepository _itemRepository;
        public ItemRetrievalService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public Task<IEnumerable<ItemEntity>> GetItems()
        {
            return _itemRepository.GetItems();
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
    }
}
