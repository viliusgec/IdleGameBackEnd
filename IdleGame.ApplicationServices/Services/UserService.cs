using IdleGame.Api.Contracts;
using IdleGame.Domain.Entities;
using IdleGame.Domain.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdleGame.ApplicationServices.Services
{
    public class UserService(IUserRetrievalService usersService, IPlayerRetrievalService playerService, ISkillRetrievalService skillService, IMappingRetrievalService mappingService) : IUserService
    {
        private readonly IUserRetrievalService _usersService = usersService;
        private readonly IPlayerRetrievalService _playerService = playerService;
        private readonly ISkillRetrievalService _skillService = skillService;
        private readonly IMappingRetrievalService _mappingService = mappingService;
        private readonly string[] _skills = {"Woodcutting", "Firemaking", "Fishing", "Cooking", "Mining", "Smithing" };

        public Task<UserEntity> GetUser(string username)
        {
            return _usersService.GetUser(username);
        }

        public async Task<string> Login(UserDto user)
        {
            var ans = await _usersService.GetUser(user.Username);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: Encoding.ASCII.GetBytes("CGYzqeN4plZekNC88Umm1Q=="),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));
            if (ans != null && ans.Password == hashed)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new("Username",user.Username.ToString()),
                        new("Role", ans.Role),
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
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: Encoding.ASCII.GetBytes("CGYzqeN4plZekNC88Umm1Q=="),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));
            user.Password = hashed;
            var newUser = await _usersService.PostUser(_mappingService.Map<UserEntity>(user));
            await _playerService.PostPlayer(user.Username);
            foreach(var skill in _skills)
            {
                await _skillService.PostSkill(user.Username, skill);
            }
            var achievements = await _skillService.GetAchievements();
            foreach(var achievement in achievements)
            {
                var ach = new PlayerAchievementsEntity { Achieved = false, Achievement = achievement, PlayerUsername = user.Username };
                await _skillService.PostPlayerAchievement(ach);
            }
            return newUser;
        }
    }
}
