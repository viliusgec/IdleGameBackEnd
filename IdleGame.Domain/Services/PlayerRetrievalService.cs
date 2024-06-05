using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;

namespace IdleGame.Domain.Services
{
    public class PlayerRetrievalService(IPlayerRepository playerRepository) : IPlayerRetrievalService
    {
        private readonly IPlayerRepository _playerRepository = playerRepository;

        public Task<PlayerEntity> PostPlayer(string username)
        {
            var player = new PlayerEntity { InventorySpace = 0, IsInAction = false, IsInBattle = false, Money = 0, Username = username };
            return _playerRepository.PostPlayer(player);
        }

        public Task<PlayerEntity> GetPlayer(string username)
        {
            return _playerRepository.GetPlayer(username);
        }

        PlayerEntity IPlayerRetrievalService.UpdatePlayer(PlayerEntity player)
        {
            return _playerRepository.UpdatePlayer(player);
        }

        public Task<IEnumerable<PlayerEntity>> GetPlayers()
        {
            return _playerRepository.GetPlayers();
        }
    }
}
