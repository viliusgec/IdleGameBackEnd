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

        public PlayerAchievementsEntity PutPlayerAchievement(PlayerAchievementsEntity achievement)
        {
            _context.Entry(_mappingService.Map<PlayerAchievementsModel>(achievement)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return achievement;
        }

        public async Task<PlayerAchievementsEntity> PostPlayerAchievement(PlayerAchievementsEntity achievement)
        {
            await _context.PlayerAchievements.AddAsync(_mappingService.Map<PlayerAchievementsModel>(achievement));
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return achievement;
        }

        public async Task<IEnumerable<PlayerAchievementsEntity>> GetPlayerAchievements(string username)
        {
            return await _context.PlayerAchievements.AsNoTracking().Where(x => x.PlayerUsername == username)
                .Join(_context.Achievements,
                    playerAchievement => playerAchievement.AchievementId,
                    achievement => achievement.Id,
                    (playerAchievement, achievement) => new PlayerAchievementsEntity
                    {
                        Id = playerAchievement.Id,
                        PlayerUsername = username,
                        Achieved = playerAchievement.Achieved,
                        Achievement = _mappingService.Map<AchievementsEntity>(achievement)
                    }
                ).ToListAsync();
        }

        public async Task<IEnumerable<AchievementsEntity>> GetAchievements()
        {
            var achievements = await _context.Achievements.ToListAsync();
            return _mappingService.Map<IEnumerable<AchievementsEntity>>(achievements);
        }

        public async Task<IEnumerable<IdleTrainingEntity>> GetIdleTrainings()
        {
            var trainings = await _context.IdleTraining.ToListAsync();
            return _mappingService.Map<IEnumerable<IdleTrainingEntity>>(trainings);
        }

        public async Task<PlayerIdleTrainingEntity> GetPlayerIdleTraining(string username)
        {
            var training = await _context.PlayerIdleTrainings.AsNoTracking().Where(x => x.PlayerUsername.Equals(username))
                .Join(_context.IdleTraining,
                    playerIdleTraining => playerIdleTraining.IdleTrainingId,
                    idleTraining => idleTraining.Id,
                    (playerIdleTraining, idleTraining) => new PlayerIdleTrainingEntity
                    {
                        Id = playerIdleTraining.Id,
                        PlayerUsername = playerIdleTraining.PlayerUsername,
                        StartTime = playerIdleTraining.StartTime,
                        Active = playerIdleTraining.Active,
                        IdleTraining = _mappingService.Map<IdleTrainingEntity>(idleTraining)
                    }
                    ).FirstAsync();
            return _mappingService.Map<PlayerIdleTrainingEntity>(training);
        }

        public async Task<PlayerIdleTrainingEntity> PutPlayerIdleTraining(PlayerIdleTrainingEntity training)
        {
            try
            {
                var b = _context.Entry(_mappingService.Map<PlayerIdleTrainingModel>(training));
                if (b.State != EntityState.Modified)
                {
                    _context.Entry(_mappingService.Map<PlayerIdleTrainingModel>(training)).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return training;
        }

        public async Task<PlayerIdleTrainingEntity> PostPlayerIdleTraining(PlayerIdleTrainingEntity training)
        {
            await _context.PlayerIdleTrainings.AddAsync(_mappingService.Map<PlayerIdleTrainingModel>(training));
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return training;
        }

        public async Task<IEnumerable<SkillEntity>> GetLeadersBySkill(string skillName, int? leadersCount)
        {
            var players = await _context.Skills.Where(x => x.Name.Equals(skillName)).OrderByDescending(x => x.Experience).Take(leadersCount ?? 100).ToListAsync();
            return _mappingService.Map<IEnumerable<SkillEntity>>(players);
        }

        public async Task<IEnumerable<PlayerStatisticsEntity>> GetPlayerStatistics(string playerUsername)
        {
            var playerStatistics = await _context.PlayerStatistics.Where(x => x.PlayerUsername.Equals(playerUsername)).ToListAsync();
            return _mappingService.Map<IEnumerable<PlayerStatisticsEntity>>(playerStatistics);
        }

        public async Task<PlayerStatisticsEntity> PostPlayerStatistic(PlayerStatisticsEntity statistic)
        {
            await _context.PlayerStatistics.AddAsync(_mappingService.Map<PlayerStatisticsModel>(statistic));
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return statistic;
        }

        public PlayerStatisticsEntity PutPlayerStatistics(PlayerStatisticsEntity statistic)
        {
            _context.Entry(_mappingService.Map<PlayerStatisticsModel>(statistic)).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return statistic;

        }
    }
}
