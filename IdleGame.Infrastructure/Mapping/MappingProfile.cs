using AutoMapper;
using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;
using IdleGame.Infrastructure.Models;

namespace IdleGame.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<UserModel, UserEntity>().ReverseMap();
            CreateMap<PlayerEntity, PlayerDto>().ReverseMap();
            CreateMap<PlayerModel, PlayerEntity>().ReverseMap();
            CreateMap<SkillEntity, SkillDto>();
            CreateMap<SkillModel, SkillEntity>().ReverseMap();
            CreateMap<TrainingSkillEntity, TrainingSkillDto>();
            CreateMap<TrainingSkillModel, TrainingSkillEntity>().ReverseMap();
            CreateMap<ItemEntity, ItemDto>().ReverseMap();
            CreateMap<ItemModel, ItemEntity>().ReverseMap();
            CreateMap<PlayerItemEntity, PlayerItemDto>()
                .ForMember(destination => destination.ItemName, opts => opts.MapFrom(source => source.Item.Name))
                .ForMember(destination => destination.Level, opts => opts.MapFrom(source => source.Item.Level))
                .ForMember(destination => destination.Description, opts => opts.MapFrom(source => source.Item.Description))
                .ForMember(destination => destination.Price, opts => opts.MapFrom(source => source.Item.Price));
            CreateMap<PlayerItemEntity, PlayerItemModel>()
                .ForMember(destination => destination.ItemName, opts => opts.MapFrom(source => source.Item.Name));
        }
    }
}
