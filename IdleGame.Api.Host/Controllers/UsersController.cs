using IdleGame.Api.Contracts;
using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdleGame.Api.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, IMappingRetrievalService mappingService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IMappingRetrievalService _mappingService = mappingService;

        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            string username = User.Claims.First(c => c.Type == "Username").Value;
            var result = await _userService.GetUser(username);
            return Ok(_mappingService.Map<UserDto>(result));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> Login(UserDto user)
        {
            var token = await _userService.Login(user);
            if(token == "")
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
            return Ok(new { token });
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<UserDto>> Register(UserDto user)
        {
            var newUser = await _userService.Register(user);
            if (newUser == null)
            {
                return Conflict();
            }
            return CreatedAtAction("Register", _mappingService.Map<UserDto>(newUser));
        }

    }
}
