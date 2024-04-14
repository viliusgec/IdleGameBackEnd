using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame.ApplicationServices.Services
{
    public class PvPService : IPvPService
    {
        private readonly IPvPRetrievalService _PvPRetrievalService;
        private readonly IPlayerRetrievalService _playerService;
        private readonly IMappingRetrievalService _mappingService;
        public PvPService(IPvPRetrievalService pvPRetrievalService, IMappingRetrievalService mappingService, IPlayerRetrievalService playerService)
        {
            _PvPRetrievalService = pvPRetrievalService;
            _mappingService = mappingService;
            _playerService = playerService;
        }
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
