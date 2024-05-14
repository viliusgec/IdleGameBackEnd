using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;

namespace IdleGame.Domain.Services
{
    public class UserRetrievalService(IUserRepository userRepository) : IUserRetrievalService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public Task<UserEntity> GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }
        public Task<UserEntity> PostUser(UserEntity user)
        {
            user.Role = "Player";
            return _userRepository.PostUser(user);
        }
    }
}
