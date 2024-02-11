using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace IdleGame.Infrastructure.Repositories
{
    public  class ItemRepository : IItemRepository
    {
        private readonly DatabaseContext.DatabaseContext _context;
        private readonly IMappingRetrievalService _mappingService;
        public ItemRepository(DatabaseContext.DatabaseContext context, IMappingRetrievalService mappingService)
        {
            _context = context;
            _mappingService = mappingService;
        }

        public async Task<IEnumerable<ItemEntity>> GetItems()
        {
            var items = await _context.Items.AsNoTracking().ToListAsync();
            return _mappingService.Map<IEnumerable<ItemEntity>>(items);
        }

        public async Task<IEnumerable<ItemEntity>> GetShopItems()
        {
            var items = await _context.ShopItems.AsNoTracking().Join(_context.Items,
                shopItems => shopItems.ItemName,
                item => item.Name,
                (userItem, item) => item).ToListAsync();
            return _mappingService.Map<IEnumerable<ItemEntity>>(items);
        }

        public async Task<IEnumerable<PlayerItemEntity>> GetPlayerItems(string username)
        {
            return await _context.PlayerItems.AsNoTracking().Where(x => x.PlayerUsername == username)
                .Join(_context.Items,
                    userItems => userItems.ItemName,
                    item => item.Name,
                    (userItem, item) => new PlayerItemEntity
                    {
                        Id = userItem.Id,
                        PlayerUsername = username,
                        IsEquiped = userItem.IsEquiped,
                        Amount = userItem.Amount,
                        Item = new ItemEntity
                        {
                            Name = userItem.ItemName,
                            Level = item.Level,
                            Description = item.Description,
                            Price = item.Price,
                            Type = item.Type,
                            Attack = item.Attack,
                            Defense = item.Defense,
                            HP = item.HP                            
                        }
                    }
                ).ToListAsync();
        }

        public async Task<PlayerItemEntity> GetPlayerItem(string username, string itemName)
        {
            return await _context.PlayerItems.AsNoTracking().Where(x => x.PlayerUsername == username && x.ItemName == itemName)
                .Join(_context.Items,
                    userItems => userItems.ItemName,
                    item => item.Name,
                    (userItem, item) => new PlayerItemEntity
                    {
                        Id = userItem.Id,
                        PlayerUsername = username,
                        IsEquiped = userItem.IsEquiped,
                        Amount = userItem.Amount,
                        Item = new ItemEntity
                        {
                            Name = userItem.ItemName,
                            Level = item.Level,
                            Description = item.Description,
                            Price = item.Price,
                            isSellable = item.isSellable,
                            Type = item.Type,
                        }
                    }
                ).FirstOrDefaultAsync();
        }

        public async Task<PlayerItemEntity> PostPlayerItem(PlayerItemEntity playerItem)
        {
            await _context.PlayerItems.AddAsync(_mappingService.Map<PlayerItemModel>(playerItem));
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return playerItem;
        }

        public PlayerItemEntity PutPlayerItem(PlayerItemEntity playerItem)
        {
            _context.Entry(_mappingService.Map<PlayerItemModel>(playerItem)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return playerItem;
        }

        public async Task<IEnumerable<MarketItemEntity>> GetMarketItems()
        {
            var items = await _context.MarketItems.AsNoTracking().ToListAsync();
            return _mappingService.Map<IEnumerable<MarketItemEntity>>(items);
        }

        public async Task<IEnumerable<MarketItemEntity>> GetPlayerMarketItems(string username)
        {
            var items = await _context.MarketItems.AsNoTracking().Where(x => x.Player.Equals(username)).ToListAsync();
            return _mappingService.Map<IEnumerable<MarketItemEntity>>(items);
        }

        public async Task<MarketItemEntity> PostMarketItem(MarketItemEntity item)
        {
            await _context.MarketItems.AddAsync(_mappingService.Map<MarketItemModel>(item));
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return item;
        }

        public MarketItemEntity PutMarketItem(MarketItemEntity item)
        {
            _context.Entry(_mappingService.Map<MarketItemModel>(item)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return item;
        }

        public MarketItemEntity DeleteMarketItem(MarketItemEntity item)
        {
            _context.Entry(_mappingService.Map<MarketItemModel>(item)).State = EntityState.Deleted;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return item;
        }

        public async Task<IEnumerable<EquippedItemsEntity>> GetPlayerEquippedItems(string username)
        {
            var equippedItems = await _context.EquippedItems.AsNoTracking().Where(x => x.PlayerUsername.Equals(username)).ToListAsync();
            return _mappingService.Map<IEnumerable<EquippedItemsEntity>>(equippedItems);
        }

        public async Task<EquippedItemsEntity> GetPlayerEquippedItem(string username, string itemType)
        {
            var equippedItem = await _context.EquippedItems.FirstOrDefaultAsync(x => x.PlayerUsername.Equals(username) && x.ItemPlace.Equals(itemType));
            return _mappingService.Map<EquippedItemsEntity>(equippedItem);
        }

        public EquippedItemsEntity PutEquippedItem(EquippedItemsEntity item)
        {
            _context.Entry(_mappingService.Map<EquippedItemsModel>(item)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return item;
        }

        public async Task<EquippedItemsEntity> PostEquippedItem(EquippedItemsEntity item)
        {
            await _context.EquippedItems.AddAsync(_mappingService.Map<EquippedItemsModel>(item));
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return item;
        }
    }
}
