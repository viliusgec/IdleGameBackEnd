using IdleGame.Domain.Entities;

namespace IdleGame.ApplicationServices.Services
{
    public interface IItemService
    {
        public Task<IEnumerable<ItemEntity>> GetItems();
        public Task<IEnumerable<PlayerItemEntity>> GetPlayerItems(string username);
    }
}
