using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Repositories
{
    public interface IPlayerRepository
    {
        public Task<PlayerEntity> PostPlayer(PlayerEntity user);
        public Task<PlayerEntity> GetPlayer(string username);
        PlayerEntity UpdatePlayer(PlayerEntity player);
        public Task<IEnumerable<PlayerEntity>> GetPlayers();
    }
}
