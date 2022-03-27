using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Services
{
    public interface IUserRetrievalService
    {
        public Task<UserEntity> GetUser(string username);
        public Task<UserEntity> PostUser(UserEntity user);
    }
}
