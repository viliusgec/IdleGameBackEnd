using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;

namespace IdleGame.ApplicationServices.Services
{
    public class BattleService(IItemRetrievalService itemService, IBattleRetrievalService battleService) : IBattleService
    {
        private readonly IItemRetrievalService _itemService = itemService;
        private readonly IBattleRetrievalService _battleService = battleService;

        public async Task<BattleEntity> ContinueBattle(string playerName, BattleEntity startedBattle)
        {
            var battle = await _battleService.GetBattle(startedBattle.ID);
            if (battle == null)
                return null;
            if (!playerName.Equals(battle.Player) || battle.BattleFinished || battle.MonsterHP <= 0)
                return null;
            var monster = await _battleService.GetMonster(battle.Monster);
            var ran = new Random();
            battle.MonsterHP -= ran.Next(10);
            if(battle.MonsterHP <= 0)
            {
                battle.BattleFinished = true;
                // give xp and item
                if(ran.Next(100) <= monster.ItemDropChance)
                {
                    battle.ItemGiven = true;
                    
                    var playerItem = await _itemService.GetPlayerItem(playerName, monster.DroppedItem);
                    if (playerItem == null)
                    {
                        var newPlayerItem = new PlayerItemEntity
                        {
                            PlayerUsername = playerName,
                            Amount = 1,
                            Item = new ItemEntity { Name = monster.DroppedItem }
                        };
                        await _itemService.PostPlayerItem(newPlayerItem);
                    }
                    else
                    {
                        playerItem.Amount += 1;
                        _itemService.PutPlayerItem(playerItem);
                    }
                }
                else
                {
                    battle.ItemGiven = false;
                }
            }
            else
            {
                battle.PlayerHP -= ran.Next(monster.Attack);
                if (battle.PlayerHP <= 0)
                {
                    battle.BattleFinished = true;
                }
            }
            _battleService.PutBattle(battle);
            return battle;
        }

        public Task<MonsterEntity> GetMonster(string name)
        {
            return _battleService.GetMonster(name);
        }

        public Task<IEnumerable<MonsterEntity>> GetMonsters()
        {
            return _battleService.GetMonsters();
        }

        public async Task<BattleEntity> StartBattle(string playerName, string monsterName)
        {
            var monster = await _battleService.GetMonster(monsterName);
            var battle = new BattleEntity { BattleFinished = false, Monster = monsterName, MonsterHP = monster.HP, Player = playerName, PlayerHP = 100 };
            return await _battleService.PostBattle(battle);
        }
    }
}
