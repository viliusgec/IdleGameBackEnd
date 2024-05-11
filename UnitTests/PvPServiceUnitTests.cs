using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using IdleGame.Domain.Entities;
using Moq;
using IdleGame.Api.Contracts;

namespace UnitTests
{
    public class PvPServiceUnitTests
    {
        private readonly PvPService pvpService;
        private readonly Mock<IPlayerRetrievalService> _playerServiceMock;
        private readonly Mock<IPvPRetrievalService> _pvpServiceMock;
        private readonly Mock<IMappingRetrievalService> _mapRetrievalServiceMock;
        public PvPServiceUnitTests()
        {
            _playerServiceMock = new Mock<IPlayerRetrievalService>();
            _pvpServiceMock = new Mock<IPvPRetrievalService>();
            _mapRetrievalServiceMock = new Mock<IMappingRetrievalService>();
            pvpService = new PvPService(_pvpServiceMock.Object, _mapRetrievalServiceMock.Object, _playerServiceMock.Object);
        }
        [Fact]
        public async void Test_CreatePvP_Should_Return_PvP()
        {
            var pvp = new PvPDto { Bet = 100, PlayerOne = "TestPlayer1", PlayerTwo = "TestPlayer2", Winner = "TestPlayer1" };
            _pvpServiceMock.Setup(x => x.CreatePvP(It.IsAny<PvPEntity>())).Returns(Task.FromResult(new PvPEntity { Bet = 100, PlayerOne = "TestPlayer1", PlayerTwo = "TestPlayer2", Winner = "TestPlayer1" }));
            var result = await pvpService.CreatePvP(pvp);
            Assert.NotNull(result);
            Assert.Equal(pvp.Bet, result.Bet);
            Assert.Equal(pvp.PlayerOne, result.PlayerOne);
            Assert.Equal(pvp.PlayerTwo, result.PlayerTwo);
            Assert.Equal(pvp.Winner, result.Winner);
        }

        [Fact]
        public async void Test_GetPvP_Should_Return_PvP()
        {
            var pvp = new PvPEntity { Bet = 100, PlayerOne = "TestPlayer1", PlayerTwo = "TestPlayer2", Winner = "TestPlayer1" };
            _pvpServiceMock.Setup(x => x.GetPvP(0)).Returns(Task.FromResult(pvp));
            var result = await pvpService.GetPvP(0);
            Assert.NotNull(result);
            Assert.Equal(pvp.Bet, result.Bet);
            Assert.Equal(pvp.PlayerOne, result.PlayerOne);
            Assert.Equal(pvp.PlayerTwo, result.PlayerTwo);
            Assert.Equal(pvp.Winner, result.Winner);
        }

        [Fact]
        public async void Test_UpdatePvP_Should_Return_PvP()
        {
            var pvp = new PvPDto { Bet = 100, PlayerOne = "TestPlayer1", PlayerTwo = "TestPlayer2", Winner = "TestPlayer1" };
            var playerOne = new PlayerEntity { Money = 100, Username = "TestPlayer1" };
            var playerTwo = new PlayerEntity { Money = 100, Username = "TestPlayer2" };
            _playerServiceMock.Setup(x => x.GetPlayer("TestPlayer1")).Returns(Task.FromResult(playerOne));
            _playerServiceMock.Setup(x => x.GetPlayer("TestPlayer2")).Returns(Task.FromResult(playerTwo));
            _pvpServiceMock.Setup(x => x.UpdatePvP(It.IsAny<PvPEntity>())).Returns(new PvPEntity { Bet = 100, PlayerOne = "TestPlayer1", PlayerTwo = "TestPlayer2", Winner = "TestPlayer1" });
            var result = await pvpService.UpdatePvP(pvp);
            Assert.NotNull(result);
            Assert.Equal(pvp.Bet, result.Bet);
            Assert.Equal(pvp.PlayerOne, result.PlayerOne);
            Assert.Equal(pvp.PlayerTwo, result.PlayerTwo);
            Assert.Equal(pvp.Winner, result.Winner);
            Assert.Equal(200, playerOne.Money);
            Assert.Equal(0, playerTwo.Money);
        }
        [Fact]
        public async void Test_UpdatePvP_Should_Return_PvP_If_PlayerTwoWins()
        {
            var pvp = new PvPDto { Bet = 100, PlayerOne = "TestPlayer1", PlayerTwo = "TestPlayer2", Winner = "TestPlayer2" };
            var playerOne = new PlayerEntity { Money = 100, Username = "TestPlayer1" };
            var playerTwo = new PlayerEntity { Money = 100, Username = "TestPlayer2" };
            _playerServiceMock.Setup(x => x.GetPlayer("TestPlayer1")).Returns(Task.FromResult(playerOne));
            _playerServiceMock.Setup(x => x.GetPlayer("TestPlayer2")).Returns(Task.FromResult(playerTwo));
            _pvpServiceMock.Setup(x => x.UpdatePvP(It.IsAny<PvPEntity>())).Returns(new PvPEntity { Bet = 100, PlayerOne = "TestPlayer1", PlayerTwo = "TestPlayer2", Winner = "TestPlayer2" });
            var result = await pvpService.UpdatePvP(pvp);
            Assert.NotNull(result);
            Assert.Equal(pvp.Bet, result.Bet);
            Assert.Equal(pvp.PlayerOne, result.PlayerOne);
            Assert.Equal(pvp.PlayerTwo, result.PlayerTwo);
            Assert.Equal(pvp.Winner, result.Winner);
            Assert.Equal(0, playerOne.Money);
            Assert.Equal(200, playerTwo.Money);
        }
    }
}
