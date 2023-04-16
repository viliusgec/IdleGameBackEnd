using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Repositories
{
    public interface IBattlesRepository
    {
        public Task<BattleEntity> PostBattle(BattleEntity battle);
        public Task<BattleEntity> GetBattle(int id);
        BattleEntity PutBattle (BattleEntity batttle);
        public Task<MonsterEntity> GetMonster(string name);
        public Task<IEnumerable<MonsterEntity>> GetMonsters();
    }
}
