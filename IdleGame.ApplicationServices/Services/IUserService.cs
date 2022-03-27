using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;

namespace IdleGame.ApplicationServices.Services
{
    public interface IUserService
    {
        public Task<UserEntity> GetUser(string username);
        public Task<string> Login(UserDto user);
        public Task<UserEntity> Register(UserDto user);
    }
}
