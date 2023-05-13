using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Services
{
    public interface ISkillRetrievalService
    {
        public Task<SkillEntity> PostSkill(string username, string skillName);
        public Task<IEnumerable<SkillEntity>> GetSkills(string username);
        public Task<SkillEntity> GetUserSkill(string skill, string username);
        public SkillEntity PutUserSkill(SkillEntity skill, TrainingSkillEntity trainingSkill);
        public SkillEntity PutUserSkill(SkillEntity skill);
        public Task<IEnumerable<TrainingSkillEntity>> GetTrainingSkillsBySkillName(string skillName);
        public Task<TrainingSkillEntity> GetTrainingSkill(string trainingSkillName);
        public PlayerAchievementsEntity PutPlayerAchievement(PlayerAchievementsEntity achievement);
        public Task<PlayerAchievementsEntity> PostPlayerAchievement(PlayerAchievementsEntity achievement);
        public Task<IEnumerable<PlayerAchievementsEntity>> GetPlayerAchievements(string username);
        public Task<IEnumerable<AchievementsEntity>> GetAchievements();
        public Task<IEnumerable<IdleTrainingEntity>> GetIdleTrainings();
        public Task<PlayerIdleTrainingEntity> GetPlayerIdleTraining(string username);
        public Task<PlayerIdleTrainingEntity> PutPlayerIdleTraining(PlayerIdleTrainingEntity training);
        public Task<PlayerIdleTrainingEntity> PostPlayerIdleTraining(PlayerIdleTrainingEntity training);

    }
}
