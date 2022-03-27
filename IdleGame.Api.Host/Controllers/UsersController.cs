using IdleGame.Api.Contracts;
using IdleGame.ApplicationServices.Services;
using IdleGame.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdleGame.Api.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMappingRetrievalService _mappingService;

        public UsersController(IUserService userService, IMappingRetrievalService mappingService)
        {
            _userService = userService;
            _mappingService = mappingService;
        }


        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser(string username)
        {
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
