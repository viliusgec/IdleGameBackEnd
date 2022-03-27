using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;

namespace IdleGame.ApplicationServices.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRetrievalService _itemService;
        private readonly IMappingRetrievalService _mappingService;
        public ItemService(IItemRetrievalService itemService, IMappingRetrievalService mappingService)
        {
            _itemService = itemService;
            _mappingService = mappingService;
        }
        public Task<IEnumerable<ItemEntity>> GetItems()
        {
            return _itemService.GetItems();
        }

        public Task<IEnumerable<PlayerItemEntity>> GetPlayerItems(string username)
        {
            return _itemService.GetPlayerItems(username);
        }
    }
}
