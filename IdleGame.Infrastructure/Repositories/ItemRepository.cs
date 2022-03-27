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
            var items = await _context.Items.ToListAsync();
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
                        Ammount = userItem.Ammount,
                        Item = new ItemEntity
                        {
                            Name = userItem.ItemName,
                            Level = item.Level,
                            Description = item.Description,
                            Price = item.Price
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
                        Ammount = userItem.Ammount,
                        Item = new ItemEntity
                        {
                            Name = userItem.ItemName,
                            Level = item.Level,
                            Description = item.Description,
                            Price = item.Price,
                            isSellable = item.isSellable
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
    }
}
