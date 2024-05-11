using IdleGame.Api.Contracts;
using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;
using Moq;

namespace UnitTests
{
    public class UserServiceUnitTests
    {
        private readonly UserService userService;
        private readonly Mock<IUserRetrievalService> _userServiceMock;
        private readonly Mock<ISkillRetrievalService> _skillServiceMock;
        private readonly Mock<IPlayerRetrievalService> _playerServiceMock;
        private readonly Mock<IMappingRetrievalService> _mapRetrievalServiceMock;
        private readonly UserEntity existingUser = new() { Email = "", Password = "123", Role = "", Username = "ExistingUser" };
        private readonly UserEntity newUser = new() { Email = "", Password = "123", Role = "", Username = "NewUser" };
        private readonly UserDto newUserDto = new() { Email = "", Password = "123", Role = "", Username = "NewUser" };
        private readonly List<AchievementsEntity> achievements = [new() { Description = "", Id = 0, RequiredCount = 10, Reward = 10, TrainingName = "TestTraining" }];
        public UserServiceUnitTests()
        {
            _playerServiceMock = new Mock<IPlayerRetrievalService>();
            _mapRetrievalServiceMock = new Mock<IMappingRetrievalService>();
            _userServiceMock = new Mock<IUserRetrievalService>();
            _skillServiceMock = new Mock<ISkillRetrievalService>();
            userService = new UserService(_userServiceMock.Object, _playerServiceMock.Object, _skillServiceMock.Object, _mapRetrievalServiceMock.Object);

            _userServiceMock.Setup(x => x.GetUser("ExistingUser")).ReturnsAsync(existingUser);
            _userServiceMock.Setup(x => x.GetUser("NewUser")).ReturnsAsync(newUser);
            _userServiceMock.Setup(x => x.PostUser(It.IsAny<UserEntity>())).ReturnsAsync(newUser);
            _skillServiceMock.Setup(x => x.GetAchievements()).ReturnsAsync(achievements.AsEnumerable());
        }
        [Fact]
        public async void Test_Register_Should_Return_NewUser()
        {
            _userServiceMock.Setup(x => x.GetUser(newUser.Username)).Returns(Task.FromResult<UserEntity>(null));
            var user = await userService.Register(newUserDto);
            Assert.NotNull(user);
            Assert.Equal(newUser, user);
        }
        [Fact]
        public async void Test_Register_Should_Return_Null()
        {
            var user = await userService.Register(newUserDto);
            Assert.Null(user);
        }
        [Fact]
        public async void Test_Login_Should_Return_Token()
        {
            var token = await userService.Login(newUserDto);
            Assert.NotEmpty(token);
        }
        [Fact]
        public async void Test_Login_Should_Return_Empty()
        {
            var token = await userService.Login(new UserDto { Username = "NotExistingUser", Password = "123" });
            Assert.Empty(token);
        }
        [Fact]
        public async void Test_Login_Should_Return_Empty_When_BadCredentials()
        {
            var token = await userService.Login(new UserDto { Username = "ExistingUser", Password = "1234" });
            Assert.Empty(token);
        }
        [Fact]
        public async void Test_GetUser_Should_Return_User()
        {
            var user = await userService.GetUser("ExistingUser");
            Assert.NotNull(user);
            Assert.Equal(existingUser, user);
        }
    }
}
