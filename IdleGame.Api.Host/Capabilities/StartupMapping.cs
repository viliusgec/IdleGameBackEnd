using AutoMapper;
using AutoMapper.Data;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Mapping;
using IdleGame.Infrastructure.Services;

namespace IdleGame.Api.Host.Capabilities
{
    public static class StartupMapping
    {
        public static IServiceCollection ConfigureMapping(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(ApplyProfiles)
                .AddSingleton<IMappingRetrievalService, MappingService>();
        }

        public static void ApplyProfiles(IMapperConfigurationExpression config)
        {
            config.AddDataReaderMapping();
            config.AddProfile<MappingProfile>();
        }
    }
}
