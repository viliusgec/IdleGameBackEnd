using IdleGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame.Domain.Services
{
    public interface IBattleRetrievalService
    {
        public Task<BattleEntity> PostBattle(BattleEntity battle);
        public Task<BattleEntity> GetBattle(int id);
        BattleEntity PutBattle(BattleEntity batttle);
        public Task<MonsterEntity> GetMonster(string name);
        public Task<IEnumerable<MonsterEntity>> GetMonsters();
    }
}
