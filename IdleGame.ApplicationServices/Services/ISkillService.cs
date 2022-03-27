using IdleGame.Domain.Entities;

namespace IdleGame.ApplicationServices.Services
{
    public interface ISkillService
    {
        public Task<IEnumerable<SkillEntity>> GetSkills(string username);
        public Task<IEnumerable<TrainingSkillEntity>> GetTrainingSkillsBySkillName(string skillName);
        public Task<SkillEntity> TrainSkill(string trainingSkill, string username);
    }
}
