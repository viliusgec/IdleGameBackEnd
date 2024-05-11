using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;
using Moq;

namespace UnitTests
{
    public class PlayerServiceUnitTests
    {
        private readonly PlayerService playerService;
        private readonly Mock<IPlayerRetrievalService> _playerServiceMock;
        private readonly Mock<IMappingRetrievalService> _mapRetrievalServiceMock;
        public PlayerServiceUnitTests()
        {
            _playerServiceMock = new Mock<IPlayerRetrievalService>();
            _mapRetrievalServiceMock = new Mock<IMappingRetrievalService>();
            playerService = new PlayerService(_playerServiceMock.Object, _mapRetrievalServiceMock.Object);
        }

        [Fact]
        public async void Test_StartBattle_Should_Return_NewBattle()
        {
            _playerServiceMock.Setup(x => x.GetPlayer("TestPlayer")).Returns(Task.FromResult(new PlayerEntity { Username = "TestPlayer", Money = 1, IsInAction = false, IsInBattle = true, InventorySpace = 10}));
            var player = await playerService.GetPlayer("TestPlayer");
            Assert.NotNull(player);
            Assert.Equal("TestPlayer", player.Username);
        }
    }
}
