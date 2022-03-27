using IdleGame.Domain.Entities;

namespace IdleGame.ApplicationServices.Services
{
    public interface IPlayerService
    {
        public Task<PlayerEntity> GetPlayer(string username);
    }
}
