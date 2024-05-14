using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace IdleGame.Infrastructure.Repositories
{
    public class BattlesRepository(DatabaseContext.DatabaseContext context, IMappingRetrievalService mappingService) : IBattlesRepository
    {
        private readonly DatabaseContext.DatabaseContext _context = context;
        private readonly IMappingRetrievalService _mappingService = mappingService;

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
            var battle = await _context.Battles.AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);
            return _mappingService.Map<BattleEntity>(battle);
        }
        
        public async Task<BattleEntity> PostBattle(BattleEntity battle)
        {
            // For some reason when using mapper it sets ID as 0 and EF Core won't populate it later. 
            var newBattle = new BattleModel { Player = battle.Player, BattleFinished = false, ItemGiven = battle.ItemGiven, Monster = battle.Monster, MonsterHP = battle.MonsterHP, PlayerHP = battle.PlayerHP };
            await _context.Battles.AddAsync(newBattle);
            try
            {
                await _context.SaveChangesAsync();
                var id = newBattle.ID;
                return _mappingService.Map<BattleEntity>(newBattle);
            }
            catch (DbUpdateException)
            {
                throw;
            }
            
            
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
