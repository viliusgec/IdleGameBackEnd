using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Repositories;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Repositories;

namespace IdleGame.Api.Host.Capabilities
{
    public static class StartupInjection
    {
        public static IServiceCollection ConfigureInjection(
            this IServiceCollection services)
        {
            // Inject domain services
            services.AddScoped<IUserRetrievalService, UserRetrievalService>();
            services.AddScoped<ISkillRetrievalService, SkillRetrievalService>();
            services.AddScoped<IPlayerRetrievalService, PlayerRetrievalService>();
            services.AddScoped<IItemRetrievalService, ItemRetrievalService>();

            // Inject Application Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IItemService, ItemService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            return services;
        }
    }
}
