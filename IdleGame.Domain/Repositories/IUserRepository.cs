using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUser(string username);
        public Task<UserEntity> PostUser(UserEntity user);
    }
}
