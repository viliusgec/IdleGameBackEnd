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
            CreateMap<MonsterEntity, MonsterDto>().ReverseMap();
            CreateMap<MonsterModel, MonsterEntity>().ReverseMap();
            CreateMap<BattleEntity, BattleDto>().ReverseMap();
            CreateMap<BattleModel, BattleEntity>().ReverseMap();
            CreateMap<MarketItemEntity, MarketItemDto>().ReverseMap();
            CreateMap<MarketItemModel, MarketItemEntity>().ReverseMap();
            CreateMap<PlayerItemEntity, PlayerItemDto>()
                .ForMember(destination => destination.ItemName, opts => opts.MapFrom(source => source.Item.Name))
                .ForMember(destination => destination.Level, opts => opts.MapFrom(source => source.Item.Level))
                .ForMember(destination => destination.Description, opts => opts.MapFrom(source => source.Item.Description))
                .ForMember(destination => destination.Price, opts => opts.MapFrom(source => source.Item.Price));
            CreateMap<PlayerItemEntity, PlayerItemModel>()
                .ForMember(destination => destination.ItemName, opts => opts.MapFrom(source => source.Item.Name));
            CreateMap<PlayerAchievementsEntity, PlayerAchievementsDto>()
                .ForMember(destination => destination.SkillType, opts => opts.MapFrom(source => source.Achievement.SkillType))
                .ForMember(destination => destination.RequiredXP, opts => opts.MapFrom(source => source.Achievement.RequiredXP))
                .ForMember(destination => destination.Reward, opts => opts.MapFrom(source => source.Achievement.Reward))
                .ForMember(destination => destination.Description, opts => opts.MapFrom(source => source.Achievement.Description));
            CreateMap<PlayerAchievementsEntity, PlayerAchievementsModel>()
                .ForMember(destination => destination.AchievementId, opts => opts.MapFrom(source => source.Achievement.Id));
            CreateMap<PlayerAchievementsModel, PlayerAchievementsEntity>()
                .ConstructUsing(x => new PlayerAchievementsEntity
                {
                    Id = x.Id,
                    PlayerUsername = x.PlayerUsername,
                    Achieved = x.Achieved,
                    Achievement = new AchievementsEntity
                    {
                        Id = x.AchievementId
                    }
                });

            CreateMap<SkillEntity, LeadershipDto>().ReverseMap();
            CreateMap<AchievementsEntity, AchievementsModel>().ReverseMap();
            CreateMap<IdleTrainingEntity, IdleTrainingModel>().ReverseMap();
            CreateMap<IdleTrainingDto, IdleTrainingEntity>().ReverseMap();
            CreateMap<PlayerIdleTrainingDto, PlayerIdleTrainingEntity>().ReverseMap();
            CreateMap<PlayerIdleTrainingModel, PlayerIdleTrainingEntity>()
               .ForPath(destinition => destinition.IdleTraining.Id, opts => opts.MapFrom(source => source.IdleTrainingId));
            CreateMap<PlayerIdleTrainingEntity, PlayerIdleTrainingModel>()
                .ForMember(destinition => destinition.IdleTrainingId, opts => opts.MapFrom(source => source.IdleTraining.Id));

        }
    }
}
