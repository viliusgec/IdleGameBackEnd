using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;

namespace IdleGame.ApplicationServices.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRetrievalService _itemService;
        private readonly IPlayerRetrievalService _playerService;
        private readonly IMappingRetrievalService _mappingService;
        public ItemService(IItemRetrievalService itemService, IPlayerRetrievalService playerService, IMappingRetrievalService mappingService)
        {
            _itemService = itemService;
            _mappingService = mappingService;
            _playerService = playerService;
        }
        public Task<IEnumerable<ItemEntity>> GetItems()
        {
            return _itemService.GetItems();
        }

        public Task<IEnumerable<PlayerItemEntity>> GetPlayerItems(string username)
        {
            return _itemService.GetPlayerItems(username);
        }

        public async Task<PlayerItemEntity> SellPlayerItems(string username, PlayerItemDto playerItem, int ammount)
        {
            var aplayerItem = await _itemService.GetPlayerItem(username, playerItem.ItemName);
            if (aplayerItem == null)
                return null;
            if (aplayerItem.Ammount < ammount && aplayerItem.Item.isSellable)
                return null;
            aplayerItem.Ammount -= ammount;
            aplayerItem = _itemService.PutPlayerItem(aplayerItem);
            var player = await _playerService.GetPlayer(username);
            player.Money += aplayerItem.Item.Price * ammount;
            _playerService.UpdatePlayer(player);
            return aplayerItem;
        }
    }
}
