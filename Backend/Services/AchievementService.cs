using AutoMapper;
using Data_;
using Data_.Dtos;
using Data_.Interfaces;
using Data_.Validators;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class AchievementService : IAchievementService
    {
        private IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public AchievementService(GmfctnContext Context, IMapper _Mapper)
        {
            UnitOfWork = new UnitOfWork(Context);
            Mapper = _Mapper;
        }
        public async Task DeleteAchievement(Guid Id, CancellationToken Cancel)
        {
            if ((await UnitOfWork.AchievementRepository
                   .DbSet.FirstOrDefaultAsync(item => item.Id == Id)) != null)
            {
                await UnitOfWork.AchievementRepository.Delete(Id, Cancel);
                await UnitOfWork.SaveChangesAsync(Cancel);
                return;
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
                    return;
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
                    .DbSet.FirstOrDefaultAsync(item => item.Id == Id)) == null)
                        throw new ArgumentNullException();
                    var _Achievement = await UnitOfWork.AchievementRepository.DbSet.FirstOrDefaultAsync(item => item.Id == Id);
                    Mapper.Map(Achievement, _Achievement);
                    UnitOfWork.AchievementRepository.Update(_Achievement);
                    await UnitOfWork.SaveChangesAsync(Cancel);
                    return;
                }
                else
                {
                    throw new ArgumentException();
                }
        }
    }
}
