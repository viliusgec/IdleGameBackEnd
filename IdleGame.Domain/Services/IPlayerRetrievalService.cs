using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Services
{
    public interface IPlayerRetrievalService
    {
        public Task<PlayerEntity> PostPlayer(string username);
        public Task<PlayerEntity> GetPlayer(string username);
        PlayerEntity UpdatePlayer(PlayerEntity player);
    }
}
