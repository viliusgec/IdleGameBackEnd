using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using Moq;

namespace UnitTests
{
    public  class ItemServiceUnitTests
    {
        private readonly ItemService itemService;
        private readonly Mock<IPlayerRetrievalService> _playerServiceMock;
        private readonly Mock<IItemRetrievalService> _itemServiceMock;
        private readonly Mock<IMappingRetrievalService> _mapRetrievalServiceMock;
        public ItemServiceUnitTests()
        {
            _playerServiceMock = new Mock<IPlayerRetrievalService>();
            _itemServiceMock = new Mock<IItemRetrievalService>();
            _mapRetrievalServiceMock = new Mock<IMappingRetrievalService>();
            itemService = new ItemService(_itemServiceMock.Object, _playerServiceMock.Object, _mapRetrievalServiceMock.Object);
        }
    }
}