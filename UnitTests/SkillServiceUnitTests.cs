using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using Moq;

namespace UnitTests
{
    public class SkillServiceUnitTests
    {
        private readonly SkillService skillService;
        private readonly Mock<ISkillRetrievalService> _skillServiceMock;
        private readonly Mock<IItemRetrievalService> _itemServiceMock;
        private readonly Mock<IMappingRetrievalService> _mapRetrievalServiceMock;
        public SkillServiceUnitTests()
        {
            _skillServiceMock = new Mock<ISkillRetrievalService>();
            _itemServiceMock = new Mock<IItemRetrievalService>();
            _mapRetrievalServiceMock = new Mock<IMappingRetrievalService>();
            skillService = new SkillService(_skillServiceMock.Object, _itemServiceMock.Object, _mapRetrievalServiceMock.Object);
        }
    }
}
