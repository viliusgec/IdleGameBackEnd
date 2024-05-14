using IdleGame.Api.Contracts;
using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdleGame.Api.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController(IPlayerService playerService, IMappingRetrievalService mappingService) : ControllerBase
    {
        private readonly IPlayerService _playerService = playerService;
        private readonly IMappingRetrievalService _mappingService = mappingService;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PlayerDto>> GetPlayer()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _playerService.GetPlayer(username);
            return Ok(_mappingService.Map<PlayerDto>(result));
        }
    }
}
