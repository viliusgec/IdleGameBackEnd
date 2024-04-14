using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;

namespace IdleGame.ApplicationServices.Services
{
    public interface IPvPService
    {
        public Task<PvPEntity> CreatePvP(PvPDto user);
        public Task<PvPEntity> GetPvP(int id);
        public Task<PvPEntity> UpdatePvP(PvPDto player);
    }
}
