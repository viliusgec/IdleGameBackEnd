using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace IdleGame.Infrastructure.Repositories
{
    public class BattlesRepository : IBattlesRepository
    {
        private readonly DatabaseContext.DatabaseContext _context;
        private readonly IMappingRetrievalService _mappingService;
        public BattlesRepository(DatabaseContext.DatabaseContext context, IMappingRetrievalService mappingService)
        {
            _context = context;
            _mappingService = mappingService;
        }

        public async Task<MonsterEntity> GetMonster(string name)
        {
            var monster = await _context.Monsters.FindAsync(name);
            return _mappingService.Map<MonsterEntity>(monster);
        }

        public async Task<IEnumerable<MonsterEntity>> GetMonsters()
        {
            var monsters = await _context.Monsters.AsNoTracking().ToListAsync();
            return _mappingService.Map<IEnumerable<MonsterEntity>>(monsters);
        }

        public async Task<BattleEntity> GetBattle(int id)
        {
            // check why .FindAsync(id) is not working
            var battle = await _context.Battles.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
            return _mappingService.Map<BattleEntity>(battle);
        }
        
        public async Task<BattleEntity> PostBattle(BattleEntity battle)
        {
            await _context.Battles.AddAsync(_mappingService.Map<BattleModel>(battle));
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return battle;
        }

        public BattleEntity PutBattle(BattleEntity battle)
        {
            _context.Entry(_mappingService.Map<BattleModel>(battle)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return battle;
        }
    }
}
