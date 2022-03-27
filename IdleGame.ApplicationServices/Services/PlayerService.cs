using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;

namespace IdleGame.ApplicationServices.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRetrievalService _playerService;
        private readonly IMappingRetrievalService _mappingService;
        public PlayerService(IPlayerRetrievalService playerService, IMappingRetrievalService mappingService)
        {
            _playerService = playerService;
            _mappingService = mappingService;
        }
        public Task<PlayerEntity> GetPlayer(string username)
        {
            return _playerService.GetPlayer(username);
        }
    }
}
