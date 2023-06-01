using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Repositories
{
    public interface ISkillRepository
    {
        public Task<SkillEntity> PostSkill(SkillEntity user);
        public Task<IEnumerable<SkillEntity>> GetSkills(string username);
        public Task<SkillEntity> GetUserSkill(string skillName, string username);
        public SkillEntity PutUserSkill(SkillEntity skill);
        public Task<IEnumerable<TrainingSkillEntity>> GetTrainingSkillsBySkillName(string skillName);
        public Task<TrainingSkillEntity> GetTrainingSkill(string skillName);
        public Task<PlayerAchievementsEntity> PostPlayerAchievement(PlayerAchievementsEntity achievement);
        public PlayerAchievementsEntity PutPlayerAchievement(PlayerAchievementsEntity achievement);
        public Task<IEnumerable<PlayerAchievementsEntity>> GetPlayerAchievements(string username);
        public Task<IEnumerable<AchievementsEntity>> GetAchievements();
        public Task<IEnumerable<IdleTrainingEntity>> GetIdleTrainings();
        public Task<PlayerIdleTrainingEntity> GetPlayerIdleTraining(string username);
        public Task<PlayerIdleTrainingEntity> PutPlayerIdleTraining(PlayerIdleTrainingEntity training);
        public Task<PlayerIdleTrainingEntity> PostPlayerIdleTraining(PlayerIdleTrainingEntity training);
        public Task<IEnumerable<SkillEntity>> GetLeadersBySkill(string skillName, int? leadersCount);
    }
}
