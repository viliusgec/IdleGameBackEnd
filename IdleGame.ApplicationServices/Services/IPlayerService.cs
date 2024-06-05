using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;

namespace IdleGame.ApplicationServices.Services
{
    public interface IPlayerService
    {
        public Task<PlayerEntity> GetPlayer(string username);
        public Task<IEnumerable<PlayerEntity>> GetPlayers();
        public Task<PlayerEntity> UpdateMoney(PlayerEntity player);
    }
}
