using IdleGame.Api.Contracts;
using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdleGame.Api.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IMappingRetrievalService _mappingService;

        public SkillsController(ISkillService skillService, IMappingRetrievalService mappingService)
        {
            _skillService = skillService;
            _mappingService = mappingService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetPlayerSkills()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _skillService.GetSkills(username);
            return Ok(_mappingService.Map<IEnumerable<SkillDto>>(result));
        }

        [HttpGet]
        [Route("GetTrainingSkills")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<TrainingSkillDto>>> GetTrainingSkills(string skillType)
        {
            var result = await _skillService.GetTrainingSkillsBySkillName(skillType);
            return Ok(_mappingService.Map<IEnumerable<TrainingSkillDto>>(result));
        }

        [HttpPost]
        [Route("TrainSkill")]
        [Authorize]
        public async Task<ActionResult<TrainingSkillDto>> TrainSkill(TrainingSkillDto trainingSkill)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _skillService.TrainSkill(trainingSkill.TrainingName, username) ?? null;
            if(result == null)
                return BadRequest();
            return Ok(_mappingService.Map<SkillDto>(result));
        }

        [HttpGet]
        [Route("Achievements")]
        [Authorize]
        public async Task<ActionResult<PlayerAchievementsDto>> Achievements()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _skillService.GetPlayerAchievements(username);
            if (result == null) 
                return BadRequest();
            return Ok(_mappingService.Map<IEnumerable<PlayerAchievementsDto>>(result));
        }

        [HttpPost]
        [Route("ClaimAchievement/{id}")]
        [Authorize]
        public async Task<ActionResult<PlayerAchievementsDto>> ClaimAchievement(int id)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _skillService.CollectPlayerAchievement(id, username);
            if (result == null) 
                return BadRequest();
            return Ok(_mappingService.Map<PlayerAchievementsDto>(result));
        }

        [HttpPost]
        [Route("StartIdleTraining/{id}")]
        [Authorize]
        public async Task<ActionResult<PlayerIdleTrainingDto>> StartIdleTraining(int id)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _skillService.StartIdleTraining(id, username);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<PlayerIdleTrainingDto>(result));
        }

        [HttpPost]
        [Route("StopIdleTraining")]
        [Authorize]
        public async Task<ActionResult<PlayerIdleTrainingDto>> StopIdleTraining()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _skillService.StopIdleTraining(username);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<PlayerIdleTrainingDto>(result));
        }

        [HttpGet]
        [Route("GetIdleTrainings")]
        [Authorize]
        public async Task<ActionResult<IdleTrainingDto>> GetIdleTrainings()
        {
            var result = await _skillService.GetIdleTrainings();
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<IEnumerable<IdleTrainingDto>>(result));
        }

        [HttpGet]
        [Route("GetActiveIdleTraining")]
        [Authorize]
        public async Task<ActionResult<IdleTrainingDto>> GetActiveIdleTraining()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _skillService.GetActiveIdleTraining(username);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<IEnumerable<PlayerIdleTrainingDto>>(result));
        }
    }
}
