using IdleGame.Domain.Entities;
using IdleGame.Domain.Repositories;
using IdleGame.Domain.Services;
using IdleGame.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace IdleGame.Infrastructure.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DatabaseContext.DatabaseContext _context;
        private readonly IMappingRetrievalService _mappingService;
        public SkillRepository(DatabaseContext.DatabaseContext context, IMappingRetrievalService mappingService)
        {
            _context = context;
            _mappingService = mappingService;
        }

        public async Task<IEnumerable<SkillEntity>> GetSkills(string username)
        {
            var skills = await _context.Skills.Where(x => x.PlayerUsername == username).ToListAsync();
            return _mappingService.Map<IEnumerable<SkillEntity>>(skills);
        }
        public async Task<SkillEntity> GetUserSkill(string skillName, string username)
        {
            var skill = await _context.Skills.AsNoTracking().FirstOrDefaultAsync(x => x.Name == skillName && x.PlayerUsername == username);
            return _mappingService.Map<SkillEntity>(skill);
        }

        public async Task<TrainingSkillEntity> GetTrainingSkill(string skillName)
        {
            var trainingSkill = await _context.TrainingSkills.FirstOrDefaultAsync(x => x.TrainingName == skillName);
            return _mappingService.Map<TrainingSkillEntity>(trainingSkill);
        }

        public async Task<IEnumerable<TrainingSkillEntity>> GetTrainingSkillsBySkillName(string skillName)
        {
            var trainingSkills = await _context.TrainingSkills.Where(x => x.SkillType == skillName).ToListAsync();
            return _mappingService.Map<IEnumerable<TrainingSkillEntity>>(trainingSkills);
        }

        public async Task<SkillEntity> PostSkill(SkillEntity skill)
        {
            await _context.Skills.AddAsync(_mappingService.Map<SkillModel>(skill));
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return skill;
        }

        public SkillEntity PutUserSkill(SkillEntity skill)
        {
            _context.Entry(_mappingService.Map<SkillModel>(skill)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return skill;
        }
    }
}
