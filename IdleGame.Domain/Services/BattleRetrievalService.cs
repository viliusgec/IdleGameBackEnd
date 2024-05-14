using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;

namespace IdleGame.Domain.Services
{
    public class BattleRetrievalService(IBattlesRepository battleRepository) : IBattleRetrievalService
    {
        private readonly IBattlesRepository _battleRepository = battleRepository;

        public Task<BattleEntity> GetBattle(int id)
        {
            return _battleRepository.GetBattle(id);
        }

        public Task<MonsterEntity> GetMonster(string name)
        {
            return _battleRepository.GetMonster(name);
        }

        public Task<IEnumerable<MonsterEntity>> GetMonsters()
        {
            return _battleRepository.GetMonsters();
        }

        public Task<BattleEntity> PostBattle(BattleEntity battle)
        {
            return _battleRepository.PostBattle(battle);
        }

        public BattleEntity PutBattle(BattleEntity batttle)
        {
            return _battleRepository.PutBattle(batttle);
        }
    }
}
