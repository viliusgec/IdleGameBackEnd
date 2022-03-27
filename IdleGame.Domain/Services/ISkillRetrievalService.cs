using IdleGame.Domain.Entities;

namespace IdleGame.Domain.Services
{
    public interface ISkillRetrievalService
    {
        public Task<SkillEntity> PostSkill(string username, string skillName);
        public Task<IEnumerable<SkillEntity>> GetSkills(string username);
        public Task<SkillEntity> GetUserSkill(string skill, string username);
        public SkillEntity PutUserSkill(SkillEntity skill, TrainingSkillEntity trainingSkill);
        public Task<IEnumerable<TrainingSkillEntity>> GetTrainingSkillsBySkillName(string skillName);
        public Task<TrainingSkillEntity> GetTrainingSkill(string trainingSkillName);
    }
}
