using AutoMapper;
using Data_;
using Data_.Dtos;
using Data_.Interfaces;
using Data_.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
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
        public async Task<UserWithAchievementsDTO> GetCurrentUser(string Token, CancellationToken Cancel)
        {
                var Claims = HelperService.GetClaimsFromToken(Token, Key);
                var UserId = HelperService.GetIdFromToken(Claims);

                if (UserId == null)
                {
                    throw new ArgumentNullException();
                }

                return await UserService.GetUserById(new Guid(UserId),Cancel);
        }

        public async Task<ICollection<Achievement>> GetCurrentUserAchievements(string Token, CancellationToken Cancel)
        {
                var Claims = HelperService.GetClaimsFromToken(Token, Key);

                var UserId = HelperService.GetIdFromToken(Claims);

                if (UserId == null)
                {
                    throw new ArgumentNullException();
                }

                var User = await UnitOfWork.UserRepository.DbSet
                    .Include(User => User.UserRoles)
                    .ThenInclude(UserRole => UserRole.Role)
                    .Include(User => User.UserAchievements)
                    .ThenInclude(UserAchievement => UserAchievement.Achievement)
                    .FirstOrDefaultAsync(User => User.Id == new Guid(UserId), Cancel);

                var Achievements = new List<Achievement>();

                foreach (var UserAchievement in User?.UserAchievements)
                {
                    UserAchievement.Achievement.UserAchievements = null;
                    Achievements.Add(UserAchievement.Achievement);
                }

                return Achievements;
        }

        public async Task UpdateCurrentUser(string Token, UserUpdateDTO NewUser, CancellationToken Cancel)
        {
            if (!ModelsValidator.UserUpdateIsValid(NewUser))
            {
                throw new ArgumentException();
            }

            var Claims = HelperService.GetClaimsFromToken(Token, Key);

            var UserId = HelperService.GetIdFromToken(Claims);

            if (UserId == null)
            {
                throw new ArgumentNullException();
            }

            await UserService.UpdateUser(new Guid(UserId), NewUser, Cancel);
            await UnitOfWork.SaveChangesAsync(Cancel);
        }

        public async Task ChangePassword(string Token, string OldPassword, string NewPassword, CancellationToken Cancel)
        {
            if (!ModelsValidator.RegExpPassword.IsMatch(NewPassword))
                throw new ArgumentException();

            var Claims = HelperService.GetClaimsFromToken(Token, Key);

            var UserId = HelperService.GetIdFromToken(Claims);

            if (UserId == null)
            {
                throw new ArgumentNullException();
            }

            var User = await UserService.GetUserById(new Guid(UserId), Cancel);

            string HashedOldPassword ;
            string HashedNewPassword ;

            using (SHA256 Sha256Hash = SHA256.Create())
            {
                var ByteArray = Sha256Hash.ComputeHash(Encoding.ASCII.GetBytes(OldPassword));
                HashedOldPassword = Encoding.UTF8.GetString(ByteArray, 0, ByteArray.Length);

                if (User.Password != HashedOldPassword)
                    throw new ArgumentException();

                if (OldPassword == NewPassword)
                    throw new ArgumentException();

                ByteArray = Sha256Hash.ComputeHash(Encoding.ASCII.GetBytes(NewPassword));
                HashedNewPassword = Encoding.UTF8.GetString(ByteArray, 0, ByteArray.Length);

                var _User = await UnitOfWork.UserRepository.GetById(new Guid(UserId), Cancel);
                _User.Password = HashedNewPassword;

                UnitOfWork.UserRepository.Update(_User);
                await UnitOfWork.SaveChangesAsync(Cancel);
            }
        }

    }
}
