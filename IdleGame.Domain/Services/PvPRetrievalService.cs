using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;

namespace IdleGame.Domain.Services
{
    public class PvPRetrievalService : IPvPRetrievalService
    {
        private readonly IPvPRepository _pvpRepository;
        public PvPRetrievalService(IPvPRepository pvpRepository)
        {
            _pvpRepository = pvpRepository;
        }

        public Task<PvPEntity> CreatePvP(PvPEntity pvp)
        {
            return _pvpRepository.CreatePvP(pvp);
        }

        public Task<PvPEntity> GetPvP(int id)
        {
            return _pvpRepository.GetPvP(id);
        }

        public PvPEntity UpdatePvP(PvPEntity pvp)
        {
            return _pvpRepository.UpdatePvP(pvp);
        }
    }
}
