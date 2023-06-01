using IdleGame.Domain.Entities;

namespace IdleGame.ApplicationServices.Services
{
    public interface ISkillService
    {
        public Task<IEnumerable<SkillEntity>> GetSkills(string username);
        public Task<IEnumerable<TrainingSkillEntity>> GetTrainingSkillsBySkillName(string skillName);
        public Task<SkillEntity> TrainSkill(string trainingSkill, string username);
        public Task<PlayerAchievementsEntity> CollectPlayerAchievement(int achievementId, string username);
        public Task<IEnumerable<PlayerAchievementsEntity>> GetPlayerAchievements(string username);
        public Task<PlayerIdleTrainingEntity> StartIdleTraining(int id, string username);
        public Task<PlayerIdleTrainingEntity> StopIdleTraining(string username);
        public Task<PlayerIdleTrainingEntity> StopIdleTrainingAction(string username);
        public Task<IEnumerable<IdleTrainingEntity>> GetIdleTrainings();
        public Task<PlayerIdleTrainingEntity> GetActiveIdleTraining(string username);
        public Task<IEnumerable<SkillEntity>> GetLeadersBySkill(string skillName, int? leadersCount);
    }
}
