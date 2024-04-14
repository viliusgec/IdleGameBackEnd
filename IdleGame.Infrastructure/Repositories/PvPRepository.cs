using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Models;
using IdleGame.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace IdleGame.Infrastructure.Repositories
{
    public class PvPRepository : IPvPRepository
    {

        private readonly DatabaseContext.DatabaseContext _context;
        private readonly IMappingRetrievalService _mappingService;
        public PvPRepository(DatabaseContext.DatabaseContext context, IMappingRetrievalService mappingService)
        {
            _context = context;
            _mappingService = mappingService;
        }

        /*        public Task<PlayerEntity> CreatePvP(PvPEntity user)
                {
                    throw new NotImplementedException();
                }

                public Task<PlayerEntity> GetPvP(int id)
                {
                    throw new NotImplementedException();
                }

                public PlayerEntity UpdatePvP(PvPEntity player)
                {
                    throw new NotImplementedException();
                }*/

        public async Task<PvPEntity> CreatePvP(PvPEntity pvp)
        {
            await _context.PvP.AddAsync(_mappingService.Map<PvPModel>(pvp));
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return pvp;
        }
        public async Task<PvPEntity> GetPvP(int id)
        {
            var pvp = await _context.PvP.FindAsync(id);
            return _mappingService.Map<PvPEntity>(pvp);
        }

        public PvPEntity UpdatePvP(PvPEntity player)
        {
            try
            {
                _context.Entry(_mappingService.Map<PvPModel>(player)).State = EntityState.Modified;
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
