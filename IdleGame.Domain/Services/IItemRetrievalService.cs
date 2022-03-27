using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Services
{
    public interface IItemRetrievalService
    {
        public Task<IEnumerable<ItemEntity>> GetItems();
        public Task<IEnumerable<PlayerItemEntity>> GetPlayerItems(string username);
        public Task<PlayerItemEntity> GetPlayerItem(string username, string itemName);
        public Task<PlayerItemEntity> PostPlayerItem(PlayerItemEntity playerItem);
        public PlayerItemEntity PutPlayerItem(PlayerItemEntity playerItem);
    }
}
