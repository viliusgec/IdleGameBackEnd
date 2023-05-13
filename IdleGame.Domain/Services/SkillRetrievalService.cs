using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;

namespace IdleGame.Domain.Services
{
    public class SkillRetrievalService : ISkillRetrievalService
    {
        private readonly ISkillRepository _skillRepository;
        public SkillRetrievalService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public Task<IEnumerable<SkillEntity>> GetSkills(string username)
        {
            return _skillRepository.GetSkills(username);
        }

        public Task<TrainingSkillEntity> GetTrainingSkill(string trainingSkillName)
        {
            return _skillRepository.GetTrainingSkill(trainingSkillName);
        }

        public Task<IEnumerable<TrainingSkillEntity>> GetTrainingSkillsBySkillName(string skillName)
        {
            return _skillRepository.GetTrainingSkillsBySkillName(skillName);
        }

        public Task<SkillEntity> GetUserSkill(string skill, string username)
        {
            return _skillRepository.GetUserSkill(skill, username);
        }

        public Task<SkillEntity> PostSkill(string username, string skillName)
        {
            var skill = new SkillEntity { Experience = 0, Id = 0, Name = skillName, PlayerUsername = username };
            return _skillRepository.PostSkill(skill);
        }

        public SkillEntity PutUserSkill(SkillEntity skill, TrainingSkillEntity trainingSkill)
        {
            skill.Experience += trainingSkill.XpGiven;
            return _skillRepository.PutUserSkill(skill);
        }

        public SkillEntity PutUserSkill(SkillEntity skill)
        {
            return _skillRepository.PutUserSkill(skill);
        }

        public Task<PlayerAchievementsEntity> PostPlayerAchievement(PlayerAchievementsEntity achievement)
        {
            return _skillRepository.PostPlayerAchievement(achievement);
        }

        public PlayerAchievementsEntity PutPlayerAchievement(PlayerAchievementsEntity achievement)
        {
            return _skillRepository.PutPlayerAchievement(achievement);
        }

        public Task<IEnumerable<PlayerAchievementsEntity>> GetPlayerAchievements(string username)
        {
            return _skillRepository.GetPlayerAchievements(username);
        }
        public Task<IEnumerable<AchievementsEntity>> GetAchievements()
        {
            return _skillRepository.GetAchievements();
        }
        public Task<PlayerIdleTrainingEntity> PostPlayerIdleTraining(PlayerIdleTrainingEntity training)
        {
            return _skillRepository.PostPlayerIdleTraining(training);
        }

        public Task<PlayerIdleTrainingEntity> PutPlayerIdleTraining(PlayerIdleTrainingEntity training)
        {
            return _skillRepository.PutPlayerIdleTraining(training);
        }

        public Task<PlayerIdleTrainingEntity> GetPlayerIdleTraining(string username)
        {
            return _skillRepository.GetPlayerIdleTraining(username);
        }
        public Task<IEnumerable<IdleTrainingEntity>> GetIdleTrainings()
        {
            return _skillRepository.GetIdleTrainings();
        }
/*
        Task<PlayerIdleTrainingEntity> ISkillRetrievalService.PutPlayerIdleTraining(PlayerIdleTrainingEntity training)
        {
            throw new NotImplementedException();
        }*/
    }
}
