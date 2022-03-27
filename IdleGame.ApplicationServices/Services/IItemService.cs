using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;

namespace IdleGame.ApplicationServices.Services
{
    public interface IItemService
    {
        public Task<IEnumerable<ItemEntity>> GetItems();
        public Task<IEnumerable<PlayerItemEntity>> GetPlayerItems(string username);
        public Task<PlayerItemEntity> SellPlayerItems(string username, PlayerItemDto playerItem, int ammount);
    }
}
