using IdleGame.Domain.Entities;

namespace IdleGame.ApplicationServices.Services
{
    public interface IBattleService
    {
        public Task<IEnumerable<MonsterEntity>> GetMonsters();
        public Task<MonsterEntity> GetMonster(string name);
        public Task<BattleEntity> StartBattle(string playerName, string monsterName);
        public Task<BattleEntity> ContinueBattle(string playerName, BattleEntity battle);
    }
}
