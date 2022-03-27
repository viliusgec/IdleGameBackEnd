namespace IdleGame.Domain.Services
{
    public interface IMappingRetrievalService
    {
        TDestination Map<TDestination>(object source);
    }
}
