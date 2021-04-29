using AutoMapper;
using Data_.Dtos;
using Data_.Entities;
using Data_.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data_.Constants;

namespace Services
{
    public class RequestAchievementService : IRequestAchievementService
    {
        private IConfiguration Config;
        private IUnitOfWork UnitOfWork;
        private readonly string Key;

        public RequestAchievementService(IConfiguration _Config, IUnitOfWork _UnitOfWork)
        {
            Config = _Config;
            UnitOfWork = _UnitOfWork;
            Key = Config.GetSection("JWTToken")["TokenSecretString"];
        }

        public async Task AddRequest(string Token, string Message, Guid AchievementId, CancellationToken Cancel)
        {
            var Claims = HelperService.GetClaimsFromToken(Token, Key);

            var UserId = new Guid(HelperService.GetIdFromToken(Claims));

            if (UserId == null)
            {
                throw new ArgumentNullException();
            }

            var Achievement = await UnitOfWork.AchievementRepository.GetById(AchievementId, Cancel);

            if (Achievement == null)
            {
                throw new ArgumentException();
            }

            var User = await UnitOfWork.UserRepository.GetById(UserId, Cancel);
            
            var Request = new RequestAchievement
            {
                Id = new Guid(),
                UserId = UserId,
                User = User,
                AchievementId = AchievementId,
                Achievement = Achievement,
                Message = Message
            };
            await UnitOfWork.RequestAchievementRepository.Create(Request, Cancel);
            await UnitOfWork.SaveChangesAsync(Cancel);
        }

        public Task<IEnumerable<RequestAchievement>> GetAllRequestAchievements(CancellationToken Cancel)
        {
            return UnitOfWork.RequestAchievementRepository.GetAll(Cancel);
        }

        public async Task AcceptRequest(Guid RequestAchievementId, CancellationToken Cancel)
        {
            var RequestAchievement = await UnitOfWork.RequestAchievementRepository.DbSet.AsNoTracking().FirstOrDefaultAsync( RA => RA.Id == RequestAchievementId, Cancel);
           
            if (RequestAchievement == null)
                throw new ArgumentException();
           
            var User = (await UnitOfWork.UserRepository.DbSet
                .Include(User => User.UserAchievements)
                .FirstOrDefaultAsync(User => User.Id == RequestAchievement.UserId, Cancel));
            
            if (User == null)
                throw new ArgumentException();
            
            var Achievement =
                await UnitOfWork.AchievementRepository.DbSet.AsNoTracking().FirstOrDefaultAsync(Ach => Ach.Id == RequestAchievement.AchievementId, Cancel);
           
            if (Achievement == null)
                throw new ArgumentException();
            
            if (User.UserAchievements == null)
                User.UserAchievements = new List<UserAchievement>();
           
            var Id = Guid.NewGuid();

            User.UserAchievements.Add(new UserAchievement
            {
                AchievementId = Achievement.Id,
                AddedTime = DateTime.UtcNow,
                User = User,
                UserId = User.Id,
                Achievement = Achievement,
                Id = Id
            });
           
            User.Xp += (int)Achievement.Xp;
            await UnitOfWork.SaveChangesAsync(Cancel);
            await UnitOfWork.RequestAchievementRepository.Delete(RequestAchievementId, Cancel);
            
            var Event = new Event
            {
                CreatedTime = DateTime.UtcNow,
                Description = $"User - {User.UserName} got achievement {Achievement.Name}",
                Id = new Guid(),
                Type = EventType.Records,
                User = null,
                UserId = User.Id
            };
            
            await UnitOfWork.EventRepository.Create(Event, Cancel);
            await UnitOfWork.SaveChangesAsync(Cancel);
        }

        public async Task DeclineRequest(Guid RequestAchievementId, CancellationToken Cancel)
        {
            await UnitOfWork.RequestAchievementRepository.Delete(RequestAchievementId, Cancel);
            await UnitOfWork.SaveChangesAsync(Cancel);
        }
    }
}
