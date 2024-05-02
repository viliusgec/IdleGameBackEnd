using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using Moq;

namespace UnitTests
{
    public class BattleServiceUnitTests
    {
        private readonly BattleService battleService;
        private readonly Mock<IBattleRetrievalService> _battleServiceMock;
        private readonly Mock<IItemRetrievalService> _itemServiceMock;
        public BattleServiceUnitTests()
        {
            _battleServiceMock = new Mock<IBattleRetrievalService>();
            _itemServiceMock = new Mock<IItemRetrievalService>();
            battleService = new BattleService(_itemServiceMock.Object, _battleServiceMock.Object);
        }
    }
}
