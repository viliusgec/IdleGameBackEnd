using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;
using Microsoft.IdentityModel.Tokens;

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

        public Task<IEnumerable<ItemEntity>> GetShopItems()
        {
            return _itemService.GetShopItems();
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

        public async Task<PlayerEntity> BuyItems(string username, string itemName, int ammount)
        {
            var player = await _playerService.GetPlayer(username);
            var shopItem = (await _itemService.GetShopItems()).FirstOrDefault(x => x.Name.Equals(itemName));
            if (shopItem == null)
                return null;
            if (player.Money < shopItem.SellPrice * ammount || shopItem.SellPrice == null)
                return null;
            player.Money -= (int) shopItem.SellPrice * ammount;
            _playerService.UpdatePlayer(player);
            var playerItem = await _itemService.GetPlayerItem(username, itemName);
            if (playerItem == null)
            {
                var newPlayerItem = new PlayerItemEntity
                {
                    PlayerUsername = username,
                    Ammount = ammount,
                    Item = new ItemEntity { Name = itemName }
                };
                await _itemService.PostPlayerItem(newPlayerItem);
            }
            else
            {
                playerItem.Ammount += ammount;
                _itemService.PutPlayerItem(playerItem);
            }
            return player;
        }

        public Task<IEnumerable<MarketItemEntity>> GetMarketItems()
        {
            return _itemService.GetMarketItems();
        }

        public Task<IEnumerable<MarketItemEntity>> GetPlayerMarketItems(string username)
        {
            return _itemService.GetPlayerMarketItems(username);
        }

        public async Task<MarketItemEntity> SellMarketItems(MarketItemDto item)
        {
            var playerItem = await _itemService.GetPlayerItem(item.Player, item.ItemName);
            if (playerItem == null)
                return null;
            playerItem.Ammount -= item.Ammount;
            _itemService.PutPlayerItem(playerItem);
            return await _itemService.PostMarketItem(_mappingService.Map<MarketItemEntity>(item));
        }

        public async Task<MarketItemEntity> BuyMarketItems(string username, MarketItemDto item)
        {
            var player = await _playerService.GetPlayer(username);
            // Parasyt normalu repository metoda kad gaut item pagal id
            var marketItem = (await _itemService.GetPlayerMarketItems(item.Player)).FirstOrDefault(x => x.ItemName.Equals(item.ItemName));
            if (marketItem == null)
                return null;
            if (player.Money < marketItem.Price * item.Ammount || marketItem.Ammount < item.Ammount || marketItem.ItemName.Equals(username))
                return null;
            player.Money -= marketItem.Price * item.Ammount;
            _playerService.UpdatePlayer(player);
            var playerItem = await _itemService.GetPlayerItem(username, item.ItemName);
            if (playerItem == null)
            {
                var newPlayerItem = new PlayerItemEntity
                {
                    PlayerUsername = username,
                    Ammount = item.Ammount,
                    Item = new ItemEntity { Name = item.ItemName }
                };
                await _itemService.PostPlayerItem(newPlayerItem);
            }
            else
            {
                playerItem.Ammount += item.Ammount;
                _itemService.PutPlayerItem(playerItem);
            }
            var itemOwner = await _playerService.GetPlayer(marketItem.Player);
            itemOwner.Money += marketItem.Price * item.Ammount;
            _playerService.UpdatePlayer(itemOwner);
            if (item.Ammount == marketItem.Ammount)
                _itemService.DeleteMarketItem(marketItem);
            else
            {
                marketItem.Ammount -= item.Ammount;
                _itemService.PutMarketItem(marketItem);
            }
            return _mappingService.Map<MarketItemEntity>(item);
        }
    }
}
