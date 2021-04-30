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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class AchievementService : IAchievementService
    {
        private IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public AchievementService(IUnitOfWork _UnitOfWork, IMapper _Mapper)
        {
            UnitOfWork = _UnitOfWork;
            Mapper = _Mapper;
        }
        public async Task DeleteAchievement(Guid Id, CancellationToken Cancel)
        {
            if ((await UnitOfWork.AchievementRepository
                   .DbSet.FirstOrDefaultAsync(item => item.Id == Id, Cancel)) != null)
            {
                await UnitOfWork.AchievementRepository.Delete(Id, Cancel);
                await UnitOfWork.SaveChangesAsync(Cancel);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task<Achievement> GetAchievementById(Guid Id, CancellationToken Cancel)
        {
            return await UnitOfWork.AchievementRepository.GetById(Id, Cancel);
        }

        public async Task<IEnumerable<Achievement>> GetAllAchievements(CancellationToken Cancel)
        {
            return await UnitOfWork.AchievementRepository.GetAll(Cancel);   
        }

        public async Task CreateAchievement(AchievementCreateDTO Achievement, CancellationToken Cancel)
        {
                if (ModelsValidator.AchievementIsValid((Mapper.Map<AchievementUpdateDTO>(Achievement))))
                {
                    var _Achievement = Mapper.Map<Achievement>(Achievement);
                    _Achievement.Id = new Guid();
                    await UnitOfWork.AchievementRepository.Create(_Achievement, Cancel);
                    await UnitOfWork.SaveChangesAsync(Cancel);
                }
                else
                {
                    throw new ArgumentException();
                }            
        }

        public async Task UpdateAchievement(Guid Id, AchievementUpdateDTO Achievement, CancellationToken Cancel)
        {
                if (ModelsValidator.AchievementIsValid(Achievement))
                { 
                    if ((await UnitOfWork.AchievementRepository
                    .DbSet.FirstOrDefaultAsync(item => item.Id == Id, Cancel)) == null)
                        throw new ArgumentNullException();

                    var _Achievement = await UnitOfWork.AchievementRepository.DbSet.FirstOrDefaultAsync(item => item.Id == Id, Cancel);
                    Mapper.Map(Achievement, _Achievement);
                    UnitOfWork.AchievementRepository.Update(_Achievement);
                    await UnitOfWork.SaveChangesAsync(Cancel);
                }
                else
                {
                    throw new ArgumentException();
                }
        }

        public async Task AddAchievementToUser(Guid AchievementId, Guid UserId, CancellationToken Cancel)
        {
            var Achievement = await GetAchievementById(AchievementId, Cancel);

            if (Achievement == null)
                throw new ArgumentException();

            var User = (await UnitOfWork.UserRepository.DbSet
                .Include(User => User.UserRoles)
                .ThenInclude(UserRole => UserRole.Role)
                .Include(User => User.UserAchievements)
                .ThenInclude(UserAchievement => UserAchievement.Achievement)
                .FirstOrDefaultAsync(User => User.Id == UserId, Cancel));

            if (User == null)
                throw new ArgumentException();

            if (User.UserAchievements == null)
                User.UserAchievements = new List<UserAchievement>();

            var Id = Guid.NewGuid();

            User.UserAchievements.Add(new UserAchievement
            {
                AchievementId = Achievement.Id,
                AddedTime = DateTime.UtcNow,
                User = User,
                UserId =  User.Id,
                Achievement = Achievement,
                Id = Id
            });
            User.Xp += (int)Achievement.Xp;

            var Event = new Event
            {
                CreatedTime = DateTime.UtcNow,
                Description = $"Got achievement {Achievement.Name}",
                Id = new Guid(),
                Type = EventType.Records,
                User = null,
                UserId = User.Id
            };

            await UnitOfWork.EventRepository.Create(Event, Cancel);
            await UnitOfWork.SaveChangesAsync(Cancel);
        }

        public async Task<IEnumerable<Achievement>> GetAchievementsByUserId(Guid UserId, CancellationToken Cancel)
        {
            return Mapper.Map<UserWithAchievementsDTO>(await UnitOfWork.UserRepository.DbSet
                .AsNoTracking()
                .Include(User => User.UserRoles)
                .ThenInclude(UserRole => UserRole.Role)
                .Include(User => User.UserAchievements)
                .ThenInclude(UserAchievement => UserAchievement.Achievement)
                .FirstOrDefaultAsync(User => User.Id == UserId, Cancel))?.Achievements;
        }

        public async Task<Achievement> GetAchievementByUserId(Guid UserId, Guid AchievementId, CancellationToken Cancel)
        {
            return Mapper.Map<UserWithAchievementsDTO>(await UnitOfWork.UserRepository.DbSet
                .AsNoTracking()
                .Include(User => User.UserRoles)
                .ThenInclude(UserRole => UserRole.Role)
                .Include(User => User.UserAchievements)
                .ThenInclude(UserAchievement => UserAchievement.Achievement)
                .FirstOrDefaultAsync(User => User.Id == UserId, Cancel))?.Achievements.FirstOrDefault(Achievement => Achievement.Id == AchievementId);
        }
    }
}
