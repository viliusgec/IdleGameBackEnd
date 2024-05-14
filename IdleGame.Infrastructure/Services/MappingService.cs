using AutoMapper;
using IdleGame.Domain.Services;

namespace IdleGame.Infrastructure.Services
{
    public class MappingService(IMapper mapper) : IMappingRetrievalService
    {
        private readonly IMapper _mapper = mapper;

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}
