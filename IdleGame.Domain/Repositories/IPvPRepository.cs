using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Repositories
{
    public interface IPvPRepository
    {
        public Task<PvPEntity> CreatePvP(PvPEntity pvp);
        public Task<PvPEntity> GetPvP(int id);
        PvPEntity UpdatePvP(PvPEntity player);
    }
}
