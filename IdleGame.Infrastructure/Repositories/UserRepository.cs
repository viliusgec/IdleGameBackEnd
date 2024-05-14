using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace IdleGame.Infrastructure.Repositories
{
    public class UserRepository(DatabaseContext.DatabaseContext context, IMappingRetrievalService mappingService) : IUserRepository
    {
        private readonly DatabaseContext.DatabaseContext _context = context;
        private readonly IMappingRetrievalService _mappingService = mappingService;

        public async Task<UserEntity> GetUser(string username)
        {
            var player = await _context.Users.FindAsync(username);
            return _mappingService.Map<UserEntity>(player);
        }

        public async Task<UserEntity> PostUser(UserEntity user)
        {
            await _context.Users.AddAsync(_mappingService.Map<UserModel> (user));
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return user;
        }
    }
}
