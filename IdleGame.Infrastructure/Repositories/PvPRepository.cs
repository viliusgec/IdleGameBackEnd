using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace IdleGame.Infrastructure.Repositories
{
    public class PvPRepository(DatabaseContext.DatabaseContext context, IMappingRetrievalService mappingService) : IPvPRepository
    {

        private readonly DatabaseContext.DatabaseContext _context = context;
        private readonly IMappingRetrievalService _mappingService = mappingService;

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
