using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
