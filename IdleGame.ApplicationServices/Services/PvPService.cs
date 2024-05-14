using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;

namespace IdleGame.ApplicationServices.Services
{
    public class PvPService(IPvPRetrievalService pvPRetrievalService, IMappingRetrievalService mappingService, IPlayerRetrievalService playerService) : IPvPService
    {
        private readonly IPvPRetrievalService _PvPRetrievalService = pvPRetrievalService;
        private readonly IPlayerRetrievalService _playerService = playerService;
        private readonly IMappingRetrievalService _mappingService = mappingService;

        public async Task<PvPEntity> CreatePvP(PvPDto pvp)
        {
            return await _PvPRetrievalService.CreatePvP(_mappingService.Map<PvPEntity>(pvp));
        }

        public Task<PvPEntity> GetPvP(int id)
        {
            return _PvPRetrievalService.GetPvP(id);
        }

        public async Task<PvPEntity> UpdatePvP(PvPDto pvp)
        {
            var playerOne = await _playerService.GetPlayer(pvp.PlayerOne);
            var playerTwo = await _playerService.GetPlayer(pvp.PlayerTwo);
            if (pvp.Winner.Equals(playerOne.Username))
            {
                playerOne.Money += pvp.Bet;
                playerTwo.Money -= pvp.Bet;
            }
            else
            {
                playerOne.Money -= pvp.Bet;
                playerTwo.Money += pvp.Bet;
            }
            _playerService.UpdatePlayer(playerOne);
            _playerService.UpdatePlayer(playerTwo);
            return _PvPRetrievalService.UpdatePvP(_mappingService.Map<PvPEntity>(pvp));
        }
    }
}
