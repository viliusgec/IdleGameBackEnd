using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using IdleGame.Domain.Entities;
using Moq;

namespace UnitTests
{
    public class SkillServiceUnitTests
    {
        private readonly SkillService skillService;
        private readonly Mock<ISkillRetrievalService> _skillServiceMock;
        private readonly Mock<IItemRetrievalService> _itemServiceMock;
        private readonly Mock<IMappingRetrievalService> _mapRetrievalServiceMock;

        private readonly SkillEntity userSkill = new() { Name = "TestSkill", Experience = 100, Id = 0, PlayerUsername = "TestPlayer" };
        private readonly TrainingSkillEntity trainingSkill = new() { SkillType = "TestSkill", SkillLevelRequired = 10, GivenItem = "TestItem", XpGiven = 10, NeededItem = "NeededTestItem", NeededItemAmount = 1, TrainingName = "TestTraining" };
        private readonly PlayerItemEntity playerItem = new() { PlayerUsername = "TestPlayer", Amount = 5, Item = new ItemEntity { Name = "TestItem" } };
        private readonly PlayerItemEntity neededPlayerItem = new() { PlayerUsername = "TestPlayer", Amount = 5, Item = new ItemEntity { Name = "NeededTestItem" } };
        private readonly List<PlayerStatisticsEntity> playerStatistics = [new() { PlayerUsername = "TestPlayer", TrainingName = "TestTraining", Count = 10, Id = 0 }];
        private readonly List<PlayerAchievementsEntity> playerAchievements = [new() { Id = 0, PlayerUsername = "TestPlayer", Achievement = new AchievementsEntity { Id = 0, Description = "TestAchievement", RequiredCount = 10, Reward = 10, TrainingName = "TestTraining"}, Achieved = false }];
        private readonly PlayerIdleTrainingEntity playerIdleTraining = new() { Id = 0, PlayerUsername = "TestPlayer", Active = true, IdleTraining = new IdleTrainingEntity { Id = 0, Name = "IdleTrainingTest", SkillName = "TestSkill", XpGiven = 10, XpNeeded = 10 }, StartTime = DateTime.UtcNow };
        private readonly List<IdleTrainingEntity> idleTrainingEntities = [new() { Id = 0, Name = "IdleTrainingTest", SkillName = "TestSkill", XpGiven = 10, XpNeeded = 10 }];
        public SkillServiceUnitTests()
        {
            _skillServiceMock = new Mock<ISkillRetrievalService>();
            _itemServiceMock = new Mock<IItemRetrievalService>();
            _mapRetrievalServiceMock = new Mock<IMappingRetrievalService>();
            skillService = new SkillService(_skillServiceMock.Object, _itemServiceMock.Object, _mapRetrievalServiceMock.Object);

            _skillServiceMock.Setup(x => x.GetTrainingSkill("TestTraining")).Returns(Task.FromResult(trainingSkill));
            _skillServiceMock.Setup(x => x.GetUserSkill("TestSkill", "TestPlayer")).Returns(Task.FromResult(userSkill));
            _itemServiceMock.Setup(x => x.GetPlayerItem("TestPlayer", "TestItem")).Returns(Task.FromResult(playerItem));
            _itemServiceMock.Setup(x => x.GetPlayerItem("TestPlayer", "NeededTestItem")).Returns(Task.FromResult(neededPlayerItem));
            _itemServiceMock.Setup(x => x.PutPlayerItem(It.IsAny<PlayerItemEntity>())).Returns(playerItem);
            _itemServiceMock.Setup(x => x.PostPlayerItem(It.IsAny<PlayerItemEntity>())).Returns(Task.FromResult(playerItem));
            _skillServiceMock.Setup(x => x.PutUserSkill(userSkill, trainingSkill)).Returns(new SkillEntity { Name = "TestSkill", Experience = userSkill.Experience+trainingSkill.XpGiven, Id = 0, PlayerUsername = "TestPlayer" });
            _skillServiceMock.Setup(x => x.PutUserSkill(It.IsAny<SkillEntity>())).Returns(new SkillEntity { Name = "TestSkill", Experience = 110, Id = 0, PlayerUsername = "TestPlayer" });
            _skillServiceMock.Setup(x => x.GetPlayerStatistics("TestPlayer")).Returns(Task.FromResult(playerStatistics.AsEnumerable()));
            _skillServiceMock.Setup(x => x.GetPlayerAchievements("TestPlayer")).Returns(Task.FromResult(playerAchievements.AsEnumerable()));
            _skillServiceMock.Setup(x => x.GetPlayerIdleTraining("TestPlayer")).Returns(Task.FromResult(playerIdleTraining));
        }
        [Fact]
        public async void Test_GetSkills_Should_Return_Skills()
        {   
            var skillsList = new List<SkillEntity> { userSkill };
            _skillServiceMock.Setup(x => x.GetSkills("TestPlayer")).Returns(Task.FromResult(skillsList.AsEnumerable()));
            var result = await skillService.GetSkills("TestPlayer");
            Assert.NotNull(result);
            Assert.Equal(skillsList, result);
        }

        [Fact]
        public async void Test_GetTrainingSkillsBySkillName_Should_Return_TrainingSkillsBySkillName()
        {
            var trainingSkillsList = new List<TrainingSkillEntity> { trainingSkill };
            _skillServiceMock.Setup(x => x.GetTrainingSkillsBySkillName("TestSkill")).Returns(Task.FromResult(trainingSkillsList.AsEnumerable()));
            var result = await skillService.GetTrainingSkillsBySkillName("TestSkill");
            Assert.NotNull(result);
            Assert.Equal(trainingSkillsList, result);
        }

        //Trainings part starts here
        [Fact]
        public async void Test_TrainSkill_Should_Return_Skill()
        {
            var result = await skillService.TrainSkill("TestTraining", "TestPlayer");
            Assert.NotNull(result);
            Assert.Equal(trainingSkill.SkillType, result.Name);
            Assert.Equal(110, result.Experience);
        }

        [Fact]
        public async void Test_TrainSkill_Should_Return_Null_When_NotEnoughExperience()
        {
            userSkill.Experience = 0;
            _skillServiceMock.Setup(x => x.GetUserSkill("TestSkill", "TestPlayer")).Returns(Task.FromResult(userSkill));
            var result = await skillService.TrainSkill("TestTraining", "TestPlayer");
            Assert.Null(result);
        }


        [Fact]
        public async void Test_TrainSkill_Should_Return_Null_When_NotEnoughItem()
        {
            neededPlayerItem.Amount = 0;
            _itemServiceMock.Setup(x => x.GetPlayerItem("TestPlayer", "NeededTestItem")).Returns(Task.FromResult(neededPlayerItem));
            var result = await skillService.TrainSkill("TestTraining", "TestPlayer");
            Assert.Null(result);
        }
        [Fact]
        public async void Test_TrainSkill_Should_Return_Null_When_NoNeededItem()
        {
            _itemServiceMock.Setup(x => x.GetPlayerItem("TestPlayer", "NeededTestItem")).Returns(Task.FromResult<PlayerItemEntity>(null));
            var result = await skillService.TrainSkill("TestTraining", "TestPlayer");
            Assert.Null(result);
        }

        [Fact]
        public async void Test_TrainSkill_Should_Return_Skill_When_NoGivenItem()
        {
            _itemServiceMock.Setup(x => x.GetPlayerItem("TestPlayer", "TestItem")).Returns(Task.FromResult<PlayerItemEntity>(null));
            var result = await skillService.TrainSkill("TestTraining", "TestPlayer");
            Assert.NotNull(result);
        }
        [Fact]
        public async void Test_TrainSkill_Should_Return_Skill_When_NoStatistics()
        {
            playerStatistics[0].TrainingName = "TestTraining1";
            _skillServiceMock.Setup(x => x.GetPlayerStatistics("TestPlayer")).Returns(Task.FromResult(playerStatistics.AsEnumerable()));
            var result = await skillService.TrainSkill("TestTraining", "TestPlayer");
            Assert.NotNull(result);
        }

        // Achievements part starts here
        [Fact]
        public async void Test_GetAchievements_Should_Return_Achievements()
        {
            var result = await skillService.GetPlayerAchievements("TestPlayer");
            Assert.NotNull(result);
            Assert.Equal(10, result.First().Count);
        }
        [Fact]
        public async void Test_CollectPlayerAchievement_Should_Return_Achievement()
        {
            var result = await skillService.CollectPlayerAchievement(0, "TestPlayer");
            Assert.NotNull(result);
            Assert.True(result.Achieved);
        }
        [Fact]
        public async void Test_CollectPlayerAchievement_Should_Return_Null_When_NotEnoughCount()
        {
            playerAchievements[0].Achievement.RequiredCount = 20;
            _skillServiceMock.Setup(x => x.GetPlayerAchievements("TestPlayer")).Returns(Task.FromResult(playerAchievements.AsEnumerable()));
            var result = await skillService.CollectPlayerAchievement(0, "TestPlayer");
            Assert.Null(result);
        }
        [Fact]
        public async void Test_CollectPlayerAchievement_Should_Return_Null_When_Achieved()
        {
            playerAchievements[0].Achieved = true;
            _skillServiceMock.Setup(x => x.GetPlayerAchievements("TestPlayer")).Returns(Task.FromResult(playerAchievements.AsEnumerable()));
            var result = await skillService.CollectPlayerAchievement(0, "TestPlayer");
            Assert.Null(result);
        }

        // Idle Trainings part starts here
        [Fact]
        public async void Test_GetIdleTrainings_Should_Return_IdleTrainings()
        {
            _skillServiceMock.Setup(x => x.GetIdleTrainings()).Returns(Task.FromResult(idleTrainingEntities.AsEnumerable()));
            var result = await skillService.GetIdleTrainings();
            Assert.NotNull(result);
            Assert.Equal(idleTrainingEntities, result);
        }
        [Fact]
        public async void Test_GetPlayerIdleTraining_Should_Return_IdleTraining()
        {
            var result = await skillService.GetActiveIdleTraining("TestPlayer");
            Assert.NotNull(result);
            Assert.Equal(playerIdleTraining, result);
        }
        [Fact]
        public async void Test_StartIdleTraining_Should_Return_IdleTrainings()
        {
            var result = await skillService.StartIdleTraining(0, "TestPlayer");
            Assert.NotNull(result);
            Assert.True(result.Active);
        }
        [Fact]
        public async void Test_StopIdleTrainingAction_Should_Return_IdleTraining()
        {
            var result = await skillService.StopIdleTrainingAction("TestPlayer");
            Assert.NotNull(result);
            Assert.False(result.Active);
        }
        [Fact]
        public async void Test_StopIdleTrainingAction_Should_Return_Null_When_NotActive()
        {
            playerIdleTraining.Active = false;
            _skillServiceMock.Setup(x => x.GetPlayerIdleTraining("TestPlayer")).Returns(Task.FromResult(playerIdleTraining));
            var result = await skillService.StopIdleTrainingAction("TestPlayer");
            Assert.Null(result);
        }
        [Fact]
        public async void Test_StopIdleTrainingAction_Should_Return_IdleTraining_When_DiffMoreThan1440()
        {
            playerIdleTraining.StartTime = DateTime.UtcNow.AddMinutes(-1441);
            _skillServiceMock.Setup(x => x.GetPlayerIdleTraining("TestPlayer")).Returns(Task.FromResult(playerIdleTraining));
            var result = await skillService.StopIdleTrainingAction("TestPlayer");
            Assert.NotNull(result);
        }
        [Fact]
        public async void Test_StopIdleTraining_Should_Return_IdleTraining()
        {
            var result = await skillService.StopIdleTraining("TestPlayer");
            Assert.NotNull(result);
        }
        [Fact]
        public async void Test_StopIdleTraining_Should_Return_IdleTraining_When_Active()
        {
            playerIdleTraining.Active = false;
            _skillServiceMock.Setup(x => x.GetPlayerIdleTraining("TestPlayer")).Returns(Task.FromResult(playerIdleTraining));
            var result = await skillService.StopIdleTraining("TestPlayer");
            Assert.False(result.Active);
        }
        [Fact]
        public async void Test_GetLeadersBySkill_Should_Return_Leaders()
        {
            var leaders = new List<SkillEntity> { userSkill };
            _skillServiceMock.Setup(x => x.GetLeadersBySkill("TestSkill", 1)).Returns(Task.FromResult(leaders.AsEnumerable()));
            var result = await skillService.GetLeadersBySkill("TestSkill", 1);
            Assert.NotNull(result);
            Assert.Equal(leaders, result);
        }
    }
}

