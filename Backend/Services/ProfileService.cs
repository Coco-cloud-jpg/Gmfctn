using Data_;
using Data_.Dtos;
using Data_.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class ProfileService : IProfileService
    {
        private IConfiguration Config;
        private IUserService UserService;
        private IUnitOfWork UnitOfWork;

        private readonly string Key;
         
        public ProfileService(IConfiguration _Config, IUserService _UserService, IUnitOfWork _UnitOfWork)
        {
            Config = _Config;
            UserService = _UserService;
            UnitOfWork = _UnitOfWork;
            Key = Config.GetSection("JWTToken")["TokenSecretString"];
        }
        public async Task<UserReadDTO> GetCurrentUser(string Token, CancellationToken Cancel)
        {
            try
            {
                var Claims = HelperService.GetClaimsFromToken(Token, Key);

                var UserId = HelperService.GetIdFromToken(Claims);
                if (UserId == null)
                {
                    return null;
                }
                return await UserService.GetUserById(new Guid(UserId),Cancel);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ICollection<Achievement>> GetCurrentUserAchievements(string Token, CancellationToken Cancel)
        {
            try
            {
                var Claims = HelperService.GetClaimsFromToken(Token, Key);

                var UserId = HelperService.GetIdFromToken(Claims);
                var User = await ((UnitOfWork)UnitOfWork)._Context.Users
                    .Include(User => User.UserRoles)
                    .ThenInclude(UserRole => UserRole.Role)
                    .Include(User => User.UserAchievements)
                    .ThenInclude(UserAchievement => UserAchievement.Achievement)
                    .FirstOrDefaultAsync(User => User.Id == new Guid(UserId), Cancel);
                var Achievements = new List<Achievement>();
                foreach (var UserAchievement in User?.UserAchievements)
                {
                    Achievements.Add(UserAchievement.Achievement);
                }
                return Achievements;
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
            
        }
    }
}
