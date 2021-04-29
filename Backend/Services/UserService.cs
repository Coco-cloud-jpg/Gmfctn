using AutoMapper;
using Data_;
using Data_.Constants;
using Data_.Dtos;
using Data_.Entities;
using Data_.Interfaces;
using Data_.Validators;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IMapper Mapper;
        private IUnitOfWork UnitOfWork;

        public UserService(IUnitOfWork _UnitOfWork, IMapper _Mapper)
        {
            Mapper = _Mapper;
            UnitOfWork = _UnitOfWork;
        }

        private async Task<UserRole> GenerateUserRole(User User, string Role, CancellationToken Cancel)
        {
            RoleName _RoleName = (Role == "Admin" ? RoleName.Admin : RoleName.User);
            Guid _RoleId = ( await
                            UnitOfWork
                            .RoleRepository
                            .DbSet
                            .AsNoTracking()
                            .FirstOrDefaultAsync(Item => Item.RoleName == _RoleName)).Id;

            var _Role = new Role()
            {
                RoleName = _RoleName,
                Id = _RoleId
            };
            var UserRole = new UserRole()
            {
                UserId = User.Id,
                User = User,
                RoleId = _RoleId,
                Role = _Role,
            };

            return UserRole;
        }
        private bool IsCorrectRole(UserCreateDTO User) 
        {
            return User.Roles.All(
                Item =>
                    Item == RoleName.Admin.ToString() ||
                    Item == RoleName.User.ToString());
        }
        public async Task CreateUser(UserCreateDTO User, CancellationToken Cancel)
        {
                if ( ModelsValidator.UserCreateIsValid(User) && IsCorrectRole(User))
                {
                    var _User = Mapper.Map<User>(User);
                    _User.Id = new Guid();
                    
                    using (SHA256 Sha256Hash = SHA256.Create())
                    {
                        var ByteArray = Sha256Hash.ComputeHash(Encoding.ASCII.GetBytes(_User.Password));
                        _User.Password = Encoding.UTF8.GetString(ByteArray, 0, ByteArray.Length);
                    }
                    
                    await UnitOfWork.UserRepository.Create(_User, Cancel);
                    
                    if (_User.UserRoles == null)
                        _User.UserRoles = new List<UserRole>();
                    
                    if (_User.UserAchievements == null)
                        _User.UserAchievements = new List<UserAchievement>();
                    
                    if (_User.Events == null)
                    _User.Events = new List<Event>();
                    
                    foreach (var Role in User.Roles) 
                    {
                        _User.UserRoles.Add(await GenerateUserRole(_User, Role, Cancel));
                    }

                    await UnitOfWork.SaveChangesAsync(Cancel);
                }
                else
                {
                    throw new ArgumentException();
                }
        }

        public async Task DeleteUser(Guid Id, CancellationToken Cancel)
        {
                if ((await UnitOfWork.UserRepository
                .DbSet.FirstOrDefaultAsync(item => item.Id == Id)) != null)
                {
                    await UnitOfWork.UserRepository.Delete(Id, Cancel);
                    await UnitOfWork.SaveChangesAsync(Cancel);
                }
                else
                {
                    throw new ArgumentException();
                }
        }

        public async Task<UserWithAchievementsDTO> GetUserById(Guid Id, CancellationToken Cancel)
        {
            return Mapper.Map<UserWithAchievementsDTO>(await UnitOfWork.UserRepository.DbSet
                .AsNoTracking()
                .Include(User => User.UserRoles)
                .ThenInclude(UserRole => UserRole.Role)
                .Include(User => User.UserAchievements)
                .ThenInclude(UserAchievement => UserAchievement.Achievement)
                .FirstOrDefaultAsync(User => User.Id == Id , Cancel));
        }

        public async Task<IEnumerable<UserWithAchievementsDTO>> GetAllUsers(CancellationToken Cancel)
        {
            return Mapper.Map< IEnumerable<UserWithAchievementsDTO>>(await UnitOfWork.UserRepository.DbSet
                .AsNoTracking()
                .Include(User => User.UserRoles)
                .ThenInclude(UserRole => UserRole.Role)
                .Include(User => User.UserAchievements)
                .ThenInclude(UserAchievement => UserAchievement.Achievement)
                .ToListAsync(Cancel));
        }

        public async Task<IEnumerable<UserReadShortDTO>> GetAllUsersInfo(CancellationToken Cancel)
        {
            return Mapper.Map<IEnumerable<UserReadShortDTO>>(await UnitOfWork.UserRepository.DbSet
                .AsNoTracking()
               .Include(User => User.UserRoles)
               .ThenInclude(UserRole => UserRole.Role)
               .ToListAsync(Cancel));
        }
        public async Task<UserReadShortDTO> GetUserInfoById(Guid Id, CancellationToken Cancel)
        {
            return Mapper.Map<UserReadShortDTO>(await UnitOfWork.UserRepository.DbSet
                .AsNoTracking()
                .Include(User => User.UserRoles)
                .ThenInclude(UserRole => UserRole.Role)
                .FirstOrDefaultAsync(User => User.Id == Id, Cancel));
        }

        public async Task UpdateUser(Guid Id, UserUpdateDTO User, CancellationToken Cancel)
        {
                if ( ModelsValidator.UserUpdateIsValid(User))
                {
                    if ((await UnitOfWork.UserRepository
                    .DbSet.FirstOrDefaultAsync(item => item.Id == Id)) == null)
                        throw new ArgumentNullException();

                    var _User = await UnitOfWork.UserRepository.DbSet.FirstOrDefaultAsync(item => item.Id == Id, Cancel);
                    Mapper.Map(User, _User);
                    UnitOfWork.UserRepository.Update(_User);
                    await UnitOfWork.SaveChangesAsync(Cancel);
                }
                else
                {
                    throw new ArgumentException();
                }
        }
    }
}
