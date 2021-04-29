using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data_.Entities;

namespace Services.Interfaces
{
    public interface IRequestAchievementService
    {
        Task AddRequest(string Token, string Message, Guid AchievementId, CancellationToken Cancel);
        Task<IEnumerable<RequestAchievement>> GetAllRequestAchievements(CancellationToken Cancel);
        Task AcceptRequest(Guid RequestAchievementId, CancellationToken Cancel);
        Task DeclineRequest(Guid RequestAchievementId, CancellationToken Cancel);
    }
}
