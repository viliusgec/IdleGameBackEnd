using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;
using Moq;

namespace UnitTests
{
    public class BattleServiceUnitTests
    {
        private readonly BattleService battleService;
        private readonly Mock<IBattleRetrievalService> _battleServiceMock;
        private readonly Mock<IItemRetrievalService> _itemServiceMock;
        private readonly Mock<Random> _randomMock;
        private readonly List<MonsterEntity> mockMonsters =
            [
                new() { Attack = 5, Defense = 0, DroppedItem = "coal", HP = 20, ItemDropChance = 101, Level = 1, Name = "Goblin", XpGiven = 0 },
                new() { Attack = 50, Defense = 10, DroppedItem = "coal", HP = 30, ItemDropChance = 1, Level = 1, Name = "Orc", XpGiven = 0 },
                new() { Attack = 30, Defense = 20, DroppedItem = "coal", HP = 40, ItemDropChance = 1, Level = 1, Name = "Troll", XpGiven = 0 }
            ];
        private readonly BattleEntity mockBattle = new() { BattleFinished = false, ID = 0, ItemGiven = false, Monster = "Goblin", MonsterHP = 20, Player = "TestPlayer", PlayerHP = 100 };

        public BattleServiceUnitTests()
        {
            _battleServiceMock = new Mock<IBattleRetrievalService>();
            _itemServiceMock = new Mock<IItemRetrievalService>();
            battleService = new BattleService(_itemServiceMock.Object, _battleServiceMock.Object);

            _battleServiceMock.Setup(x => x.GetMonster("Goblin")).Returns(Task.FromResult(mockMonsters[0]));
            _battleServiceMock.Setup(x => x.GetMonsters()).Returns(Task.FromResult(mockMonsters.AsEnumerable()));
            _battleServiceMock.Setup(x => x.PostBattle(It.IsAny<BattleEntity>())).Returns(Task.FromResult(mockBattle));
            _battleServiceMock.Setup(x => x.GetBattle(0)).Returns(Task.FromResult(mockBattle));
            _battleServiceMock.Setup(x => x.PutBattle(mockBattle)).Returns(mockBattle);
            _itemServiceMock.Setup(x => x.GetPlayerItem("TestPlayer", "coal")).Returns(Task.FromResult(new PlayerItemEntity { Amount = 1, Item = new ItemEntity { Name = "coal" }, PlayerUsername = "TestPlayer" }));
            _itemServiceMock.Setup(x => x.PostPlayerItem(It.IsAny<PlayerItemEntity>())).Returns(Task.FromResult(new PlayerItemEntity { Amount = 1, Item = new ItemEntity { Name = "coal" }, PlayerUsername = "TestPlayer" }));
            _itemServiceMock.Setup(x => x.PutPlayerItem(It.IsAny<PlayerItemEntity>())).Returns(new PlayerItemEntity { Amount = 1, Item = new ItemEntity { Name = "coal" }, PlayerUsername = "TestPlayer" });

        }

        [Fact]
        public async void Test_GetMonster_Should_Return_MonsterDetails()
        {
            var monster = await battleService.GetMonster("Goblin");
            Assert.NotNull(monster);
            Assert.Equal("Goblin", monster.Name);
            Assert.Equal(mockMonsters[0], monster);
        }

        [Fact]
        public async void Test_GetMonsters_Should_Return_ListOfMonsters()
        {
            var monsters = await battleService.GetMonsters();
            Assert.NotNull(monsters);
            Assert.Equal(3, monsters.Count());
            Assert.Equal(mockMonsters, monsters);
        }

        [Fact]
        public async void Test_StartBattle_Should_Return_NewBattle()
        {
            var battle = await battleService.StartBattle("TestPlayer", "Goblin");
            Assert.NotNull(battle);
        }

        [Fact]
        public async void Test_ContinueBattle_Should_Return_UpdatedBattle()
        {
            var battle = await battleService.ContinueBattle("TestPlayer", mockBattle);
            mockBattle.MonsterHP -= 5;
            mockBattle.PlayerHP -= 5;
            Assert.NotNull(battle);
            Assert.False(battle.BattleFinished);
            Assert.False(battle.ItemGiven);
            Assert.Equal(mockBattle, battle);
        }

        [Fact]
        public async void Test_ContinueBattle_Should_Return_Null_If_BattleNotFound()
        {
            mockBattle.ID = 1;
            var battle = await battleService.ContinueBattle("TestPlayer", mockBattle);
            Assert.Null(battle);
        }

        [Fact]
        public async void Test_ContinueBattle_Should_Return_Null_If_PlayerNameDoesNotMatch()
        {
            var battle = await battleService.ContinueBattle("TestPlayer1", mockBattle);
            Assert.Null(battle);
        }
        
        [Fact]
        public async void Test_ContinueBattle_Should_Return_Null_If_BattleFinished()
        {
            mockBattle.BattleFinished = true;
            var battle = await battleService.ContinueBattle("TestPlayer", mockBattle);
            Assert.Null(battle);
        }
        
        [Fact]
        public async void Test_ContinueBattle_Should_Return_Null_If_MonsterHPZero()
        {
            mockBattle.MonsterHP = 0;
            var battle = await battleService.ContinueBattle("TestPlayer", mockBattle);
            Assert.Null(battle);
        }
        
        [Fact]
        public async void Test_ContinueBattle_Should_Return_BattleEnd_If_PlayerHPZeroAfterAttack()
        {
            mockBattle.PlayerHP = 1;
            _battleServiceMock.Setup(x => x.GetBattle(0)).Returns(Task.FromResult(mockBattle));
            var battle = await battleService.ContinueBattle("TestPlayer", mockBattle);
            Assert.True(battle.BattleFinished);
            Assert.True(battle.PlayerHP <= 0);
        }
        
        [Fact]
        public async void Test_ContinueBattle_Should_Return_BattleEned_If_MonsterHPZeroAfterAttack_And_ItemGiven()
        {
            mockBattle.MonsterHP = 1;
            _battleServiceMock.Setup(x => x.GetBattle(0)).Returns(Task.FromResult(mockBattle));
            var battle = await battleService.ContinueBattle("TestPlayer", mockBattle);
            Assert.True(battle.BattleFinished);
            Assert.True(battle.ItemGiven);
            Assert.True(battle.MonsterHP <= 0);
        }
        
        [Fact]
        public async void Test_ContinueBattle_Should_Return_BattleEned_If_MonsterHPZeroAfterAttack_And_NewItemGiven()
        {
            mockBattle.MonsterHP = 1;
            _battleServiceMock.Setup(x => x.GetBattle(0)).Returns(Task.FromResult(mockBattle));
            _itemServiceMock.Setup(x => x.GetPlayerItem("TestPlayer", "coal")).Returns(Task.FromResult<PlayerItemEntity>(null));
            var battle = await battleService.ContinueBattle("TestPlayer", mockBattle);
            Assert.True(battle.BattleFinished);
            Assert.True(battle.ItemGiven);
            Assert.True(battle.MonsterHP <= 0);
        }
        
        [Fact]
        public async void Test_ContinueBattle_Should_Return_BattleEned_If_MonsterHPZeroAfterAttack_And_ItemNotGiven()
        {
            mockBattle.MonsterHP = 1;
            mockMonsters[0].ItemDropChance = 0;
            _battleServiceMock.Setup(x => x.GetMonster("Goblin")).Returns(Task.FromResult(mockMonsters[0]));
            _battleServiceMock.Setup(x => x.GetBattle(0)).Returns(Task.FromResult(mockBattle));
            var battle = await battleService.ContinueBattle("TestPlayer", mockBattle);
            Assert.True(battle.BattleFinished);
            Assert.False(battle.ItemGiven);
            Assert.True(battle.MonsterHP <= 0);
        }
    }
}
 