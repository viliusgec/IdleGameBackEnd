﻿using IdleGame.Api.Contracts;
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

        public async Task<PlayerItemEntity> SellPlayerItems(string username, PlayerItemDto playerItem, int Amount)
        {
            var aplayerItem = await _itemService.GetPlayerItem(username, playerItem.ItemName);
            if (aplayerItem == null)
                return null;
            if (aplayerItem.Amount < Amount && aplayerItem.Item.isSellable)
                return null;
            aplayerItem.Amount -= Amount;
            aplayerItem = _itemService.PutPlayerItem(aplayerItem);
            var player = await _playerService.GetPlayer(username);
            player.Money += aplayerItem.Item.Price * Amount;
            _playerService.UpdatePlayer(player);
            return aplayerItem;
        }

        public async Task<PlayerEntity> BuyItems(string username, string itemName, int Amount)
        {
            var player = await _playerService.GetPlayer(username);
            var shopItem = (await _itemService.GetShopItems()).FirstOrDefault(x => x.Name.Equals(itemName));
            if (shopItem == null)
                return null;
            if (player.Money < shopItem.SellPrice * Amount || shopItem.SellPrice == null)
                return null;
            player.Money -= (int)shopItem.SellPrice * Amount;
            _playerService.UpdatePlayer(player);
            var playerItem = await _itemService.GetPlayerItem(username, itemName);
            if (playerItem == null)
            {
                var newPlayerItem = new PlayerItemEntity
                {
                    PlayerUsername = username,
                    Amount = Amount,
                    Item = new ItemEntity { Name = itemName }
                };
                await _itemService.PostPlayerItem(newPlayerItem);
            }
            else
            {
                playerItem.Amount += Amount;
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
            playerItem.Amount -= item.Amount;
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
            if (player.Money < marketItem.Price * item.Amount || marketItem.Amount < item.Amount || marketItem.ItemName.Equals(username))
                return null;
            // Maybe sell all items for marketItem.Price? 
            player.Money -= (marketItem.Price * item.Amount);
            _playerService.UpdatePlayer(player);
            var playerItem = await _itemService.GetPlayerItem(username, item.ItemName);
            if (playerItem == null)
            {
                var newPlayerItem = new PlayerItemEntity
                {
                    PlayerUsername = username,
                    Amount = item.Amount,
                    Item = new ItemEntity { Name = item.ItemName }
                };
                await _itemService.PostPlayerItem(newPlayerItem);
            }
            else
            {
                playerItem.Amount += item.Amount;
                _itemService.PutPlayerItem(playerItem);
            }
            var itemOwner = await _playerService.GetPlayer(marketItem.Player);
            itemOwner.Money += marketItem.Price * item.Amount;
            _playerService.UpdatePlayer(itemOwner);
            if (item.Amount == marketItem.Amount)
                _itemService.DeleteMarketItem(marketItem);
            else
            {
                marketItem.Amount -= item.Amount;
                _itemService.PutMarketItem(marketItem);
            }
            return _mappingService.Map<MarketItemEntity>(item);
        }
        public async Task<MarketItemEntity> CancelMarketListing(string username, MarketItemDto item)
        {
            var player = await _playerService.GetPlayer(username);
            // Parasyt normalu repository metoda kad gaut item pagal id
            var marketItem = (await _itemService.GetPlayerMarketItems(item.Player)).FirstOrDefault(x => x.ItemName.Equals(item.ItemName));
            if (marketItem == null)
                return null;
            if (marketItem.Player != player.Username)
                return null;

            var playerItem = await _itemService.GetPlayerItem(username, marketItem.ItemName);
            if (playerItem == null)
            {
                var newPlayerItem = new PlayerItemEntity
                {
                    PlayerUsername = username,
                    Amount = marketItem.Amount,
                    Item = new ItemEntity { Name = marketItem.ItemName }
                };
                await _itemService.PostPlayerItem(newPlayerItem);
            }
            else
            {
                playerItem.Amount += marketItem.Amount;
                _itemService.PutPlayerItem(playerItem);
            }

            return _itemService.DeleteMarketItem(marketItem); ;
        }

        public Task<IEnumerable<EquippedItemsEntity>> GetPlayerEquippedItems(string username)
        {
            return _itemService.GetPlayerEquippedItems(username);
        }

        public async Task<EquippedItemsEntity> GetPlayerEquippedItem(string username, string itemType)
        {
            return await _itemService.GetPlayerEquippedItem(username, itemType);
        }
        public async Task<EquippedItemsEntity> EquipItem(string username, string itemName)
        {
            // Should get player item instead
            // [TODO]: Add validation if player can equip the item (if xp count meets requirements) 
            var item = (await _itemService.GetItems()).FirstOrDefault(x => x.Name.Equals(itemName));
            if (item == null)
                return null;
            var equippedItem = await _itemService.GetPlayerEquippedItem(username, item.Type);
            if (equippedItem == null)
                return null;
            equippedItem.Item = item;
            return _itemService.PutEquippedItem(equippedItem);
        }

        public async Task<EquippedItemsEntity> UnEquipItem(string username, string itemPlace)
        {
            var equippedItem = await _itemService.GetPlayerEquippedItem(username, itemPlace);
            equippedItem.Item = null;
            return _itemService.PutEquippedItem(equippedItem);
        }

        /* public async Task<EquippedItemsEntity> PostEquippedItem(EquippedItemsEntity item)
        {
         Create logic to add equipped item rows to db for new accounts
        } */
    }
}
