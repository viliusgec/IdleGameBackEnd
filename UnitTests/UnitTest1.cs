using IdleGame.Domain.Entities;
using IdleGame.Infrastructure.Models;
using System.Diagnostics;
using System.Net.Http;
using Moq;
using IdleGame.Domain.Services;
using IdleGame.ApplicationServices.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UnitTests
{
    public class UnitTest1
    {
        private readonly BattleService battleService;
        private readonly Mock<IBattleRetrievalService> _battleServiceMock;
        private readonly Mock<IItemRetrievalService> _itemServiceMock;

        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public async Task GivenARequest_WhenCallingGetBooks_ThenTheAPIReturnsExpectedResponse()
        {
            /*Mock<IUSD_CLP_ExchangeRateFeed> mockObject = new Mock<IUSD_CLP_ExchangeRateFeed>();
            mockObject.Setup(m => m.GetActualUSDValue()).Returns(500);
            return mockObject.Object;*/
            Mock<IBattleRetrievalService> mockObject = new();

            _battleServiceMock.Setup(m => m.GetMonster("Goblin")).Returns(Task.FromResult(new MonsterEntity { Attack = 0, Defense = 0, DroppedItem = "coal", HP = 0, ItemDropChance = 1, Level = 1, Name = "monster", XpGiven = 0 }));
            // Arrange.

            //await Assert.ThrowsAsync<NullReferenceException>(async () => await battleService.GetMonster("Goblin"));
            var a = await battleService.GetMonster("Goblin");
            var stopwatch = Stopwatch.StartNew();
            Assert.NotNull(a);
            // Act.
            // var response = await GetUser();
            // Assert.
            //await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
        }

        public UnitTest1()
        {
            _battleServiceMock = new Mock<IBattleRetrievalService>();
            _itemServiceMock = new Mock<IItemRetrievalService>();
            //public BattleService(IItemRetrievalService itemService, IBattleRetrievalService battleService)
            battleService = new BattleService(_itemServiceMock.Object, _battleServiceMock.Object);
        }
    }
}