using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdleGame.ApplicationServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRetrievalService _usersService;
        private readonly IPlayerRetrievalService _playerService;
        private readonly ISkillRetrievalService _skillService;
        private readonly IMappingRetrievalService _mappingService;
        private readonly string[] _skills = {"Woodcutting", "Firemaking", "Fishing", "Cooking", "Mining"};
        public UserService(IUserRetrievalService usersService, IPlayerRetrievalService playerService, ISkillRetrievalService skillService, IMappingRetrievalService mappingService)
        {
            _usersService = usersService;
            _playerService = playerService;
            _skillService = skillService;
            _mappingService = mappingService;
        }
        public Task<UserEntity> GetUser(string username)
        {
            return _usersService.GetUser(username);
        }

        public async Task<string> Login(UserDto user)
        {
            var ans = await _usersService.GetUser(user.Username);
            if (ans != null && ans.Password == user.Password)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Username",user.Username.ToString()),
                        new Claim("Role", ans.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567753123456")), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return token;
            }
            return "";
        }

        public async Task<UserEntity> Register(UserDto user)
        {
            var checkUser = await _usersService.GetUser(user.Username);
            if (checkUser != null) return null;
            var newUser = await _usersService.PostUser(_mappingService.Map<UserEntity>(user));
            await _playerService.PostPlayer(user.Username);
            foreach(var skill in _skills)
            {
                await _skillService.PostSkill(user.Username, skill);
            }
            return newUser;
        }
    }
}
