using IdleGame.Domain.Entities;
using IdleGame.Infrastructure.Models;
using System.Diagnostics;
using System.Net.Http;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public async Task GivenARequest_WhenCallingGetBooks_ThenTheAPIReturnsExpectedResponse()
        {
            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = new[]
            {
/*                        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }*/
                new UserEntity{ Username = "Name", Password = "Password", Email = "Email", Role = "Role" },
            };
            var stopwatch = Stopwatch.StartNew();
            // Act.
            // var response = await GetUser();
            // Assert.
            //await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
        }
    }
}