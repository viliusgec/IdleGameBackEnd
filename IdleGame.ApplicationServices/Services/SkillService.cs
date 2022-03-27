using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;

namespace IdleGame.ApplicationServices.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRetrievalService _skillService;
        private readonly IItemRetrievalService _itemRetrievalService;
        private readonly IMappingRetrievalService _mappingService;
        public SkillService(ISkillRetrievalService skillService, IItemRetrievalService itemRetrievalService, IMappingRetrievalService mappingService)
        {
            _skillService = skillService;
            _mappingService = mappingService;
            _itemRetrievalService = itemRetrievalService;
        }
        public Task<IEnumerable<SkillEntity>> GetSkills(string username)
        {
            return _skillService.GetSkills(username);
        }

        public Task<IEnumerable<TrainingSkillEntity>> GetTrainingSkillsBySkillName(string skillName)
        {
            return _skillService.GetTrainingSkillsBySkillName(skillName);
        }

        public async Task<SkillEntity> TrainSkill(string trainingSkillName, string username)
        {
            var trainingSkill = await _skillService.GetTrainingSkill(trainingSkillName);
            var userSkill = await _skillService.GetUserSkill(trainingSkill.SkillType, username);
            if (userSkill.Experience < trainingSkill.SkillLevelRequired * 10)
                return null;
            var playerItem = await _itemRetrievalService.GetPlayerItem(username, trainingSkill.GivenItem);
            if(playerItem == null)
            {
                var newPlayerItem = new PlayerItemEntity
                {
                    PlayerUsername = username,
                    Ammount = trainingSkill.GivenItemAmount ?? 1,
                    Item = new ItemEntity { Name = trainingSkill.GivenItem }
                };
                await _itemRetrievalService.PostPlayerItem(newPlayerItem);
            }
            else
            {
                playerItem.Ammount += trainingSkill.GivenItemAmount ?? 1;
                _itemRetrievalService.PutPlayerItem(playerItem);
            }
            return _skillService.PutUserSkill(userSkill, trainingSkill);
        }
    }
}
