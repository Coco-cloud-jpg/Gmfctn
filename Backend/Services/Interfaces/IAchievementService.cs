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
        Task DeleteAchievement(Guid Id, CancellationToken Cancel);
        Task UpdateAchievement(Guid Id, AchievementUpdateDTO Achievement, CancellationToken Cancel);
        Task CreateAchievement(AchievementCreateDTO Achievement, CancellationToken Cancel);
        Task AddAchievementToUser(Guid AchievementId, Guid UserId, CancellationToken Cancel);
        Task<IEnumerable<Achievement>> GetAchievementsByUserId(Guid UserId, CancellationToken Cancel);
        Task<Achievement> GetAchievementByUserId(Guid UserId, Guid AchievementId, CancellationToken Cancel);
    }
}
