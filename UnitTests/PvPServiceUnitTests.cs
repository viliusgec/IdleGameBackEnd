using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using Moq;

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
    }
}
