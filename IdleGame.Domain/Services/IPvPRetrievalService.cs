using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Services
{
    public interface IPvPRetrievalService
    {
        public Task<PvPEntity> CreatePvP(PvPEntity user);
        public Task<PvPEntity> GetPvP(int id);
        PvPEntity UpdatePvP(PvPEntity player);
    }
}
