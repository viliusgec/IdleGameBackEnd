using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace IdleGame.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DatabaseContext.DatabaseContext _context;
        private readonly IMappingRetrievalService _mappingService;
        public PlayerRepository(DatabaseContext.DatabaseContext context, IMappingRetrievalService mappingService)
        {
            _context = context;
            _mappingService = mappingService;
        }

        public async Task<PlayerEntity> PostPlayer(PlayerEntity player)
        {
            await _context.Players.AddAsync(_mappingService.Map<PlayerModel>(player));
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return player;
        }
        public async Task<PlayerEntity> GetPlayer(string username)
        {
            var player = await _context.Players.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Username == username);
            return _mappingService.Map<PlayerEntity>(player);
        }

        public PlayerEntity UpdatePlayer(PlayerEntity player)
        {
            try
            {
                _context.Entry(_mappingService.Map<PlayerModel>(player)).State = EntityState.Modified;
                var a = "test";
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch
            {
                throw;
            }
            return player;
        }
    }
}
