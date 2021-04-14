using Data_;
using Data_.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAchievementService
    {
        Task<Achievement> GetAchievementById(Guid Id, CancellationToken Cancel);
        Task<IEnumerable<Achievement>> GetAllAchievements(CancellationToken Cancel);
        Task<bool> DeleteAchievement(Guid Id, CancellationToken Cancel);
        Task<bool> UpdateAchievement(Guid Id, AchievementUpdateDTO Achievement, CancellationToken Cancel);
        Task<bool> CreateAchievement(AchievementCreateDTO Achievement, CancellationToken Cancel);
    }
}
