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
    }
}
