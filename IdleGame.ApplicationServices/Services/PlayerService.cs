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

        public Task<IEnumerable<PlayerEntity>> GetPlayers()
        {
            return _playerService.GetPlayers();
        }

        public async Task<PlayerEntity> UpdateMoney(PlayerEntity player)
        {
            var playerData = await _playerService.GetPlayer(player.Username);
            playerData.Money = player.Money;
            return _playerService.UpdatePlayer(playerData);
        }
    }
}
