using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;

namespace IdleGame.ApplicationServices.Services
{
    public class PlayerService(IPlayerRetrievalService playerService) : IPlayerService
    {
        private readonly IPlayerRetrievalService _playerService = playerService;

        public Task<PlayerEntity> GetPlayer(string username)
        {
            return _playerService.GetPlayer(username);
        }
    }
}
