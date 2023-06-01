using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;
using Microsoft.IdentityModel.Tokens;

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
            // add needed item validation

            if (!trainingSkill.NeededItem.IsNullOrEmpty())
            {
                var neededPlayerItem = await _itemRetrievalService.GetPlayerItem(username, trainingSkill.NeededItem);
                if (neededPlayerItem == null)
                {
                    return null;
                }
                if (neededPlayerItem.Amount < trainingSkill.NeededItemAmount)
                {
                    return null;
                }
                neededPlayerItem.Amount -= trainingSkill.NeededItemAmount ?? 1;
                _itemRetrievalService.PutPlayerItem(neededPlayerItem);
            }
            var playerItem = await _itemRetrievalService.GetPlayerItem(username, trainingSkill.GivenItem);
            if (playerItem == null)
            {
                var newPlayerItem = new PlayerItemEntity
                {
                    PlayerUsername = username,
                    Amount = trainingSkill.GivenItemAmount ?? 1,
                    Item = new ItemEntity { Name = trainingSkill.GivenItem }
                };
                await _itemRetrievalService.PostPlayerItem(newPlayerItem);
            }
            else
            {
                playerItem.Amount += trainingSkill.GivenItemAmount ?? 1;
                _itemRetrievalService.PutPlayerItem(playerItem);
            }
            return _skillService.PutUserSkill(userSkill, trainingSkill);
        }

        public async Task<PlayerAchievementsEntity> CollectPlayerAchievement(int achievementId, string username)
        {
            var achievement = (await _skillService.GetPlayerAchievements(username)).First(x => x.Achievement.Id == achievementId);
            var playerSkill = await _skillService.GetUserSkill(achievement.Achievement.SkillType, username);
            if (achievement.Achievement.RequiredXP > playerSkill.Experience)
                return null;
            if (achievement.Achieved)
                return null;
            playerSkill.Experience += achievement.Achievement.Reward;
            _skillService.PutUserSkill(playerSkill);
            achievement.Achieved = true;
            _skillService.PutPlayerAchievement(achievement);
            return achievement;
        }

        public Task<IEnumerable<PlayerAchievementsEntity>> GetPlayerAchievements(string username)
        {
            return _skillService.GetPlayerAchievements(username);
        }

        // Call what ever u want
        public async Task<PlayerIdleTrainingEntity> StartIdleTraining(int id, string username)
        {
            var training = await _skillService.GetPlayerIdleTraining(username);
            if (training.Active)
                await StopIdleTrainingAction(username);
            //get training and check if player can start it
            training.IdleTraining.Id = id;
            training.StartTime = DateTime.UtcNow;
            training.Active = true;
            await _skillService.PutPlayerIdleTraining(training);
            return training;
        }

        public async Task<PlayerIdleTrainingEntity> StopIdleTrainingAction(string username)
        {
            var training = await _skillService.GetPlayerIdleTraining(username);
            if (!training.Active)
                return null;
            var playerSkill = await _skillService.GetUserSkill(training.IdleTraining.SkillName, username);
            var diff = (DateTime.UtcNow - training.StartTime).TotalMinutes;
            if (diff > 1440)
                diff = 1440;
            playerSkill.Experience += ((int)diff * training.IdleTraining.XpGiven);
            _skillService.PutUserSkill(playerSkill); 
            training.Active = false;
            //await _skillService.PutPlayerIdleTraining(training);
            return training;
        }

        public async Task<PlayerIdleTrainingEntity> StopIdleTraining(string username)
        {
            var training = await _skillService.GetPlayerIdleTraining(username);
            if (training.Active)
                training = await StopIdleTrainingAction(username);
            await _skillService.PutPlayerIdleTraining(training);
            return training;
        }

        public async Task<IEnumerable<IdleTrainingEntity>> GetIdleTrainings()
        {
            return await _skillService.GetIdleTrainings();
        }

        public async Task<PlayerIdleTrainingEntity> GetActiveIdleTraining(string username)
        {
            return await _skillService.GetPlayerIdleTraining(username);
        }

        public async Task<IEnumerable<SkillEntity>> GetLeadersBySkill(string skillName, int? leadersCount)
        {
            return await _skillService.GetLeadersBySkill(skillName, leadersCount);
        }
    }
}
