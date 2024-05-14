using IdleGame.Api.Contracts;
using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdleGame.Api.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattlesController(IBattleService battleService, IMappingRetrievalService mappingService) : ControllerBase
    {
        private readonly IBattleService _battleService = battleService;
        private readonly IMappingRetrievalService _mappingService = mappingService;

        [HttpGet]
        [Route("Monsters")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MonsterDto>>> GetMonsters()
        {
            var result = await _battleService.GetMonsters();
            return Ok(_mappingService.Map<IEnumerable<MonsterDto>>(result));
        }

        [HttpPost]
        [Route("StartBattle")]
        [Authorize]
        public async Task<ActionResult<BattleDto>> StartBattle(string monsterName)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _battleService.StartBattle(username, monsterName);
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<BattleDto>(result));
        }

        [HttpPost]
        [Route("ContinueBattle")]
        [Authorize]
        public async Task<ActionResult<BattleDto>> ContinueBattle(BattleDto battle)
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _battleService.ContinueBattle(username, _mappingService.Map<BattleEntity>(battle));
            if (result == null)
                return BadRequest();
            return Ok(_mappingService.Map<BattleDto>(result));
        }
    }
}
