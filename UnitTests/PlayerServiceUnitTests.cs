using IdleGame.ApplicationServices.Services;
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
    }
}
