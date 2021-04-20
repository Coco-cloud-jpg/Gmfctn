using Data_;
using Data_.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProfileService
    {
        Task<UserReadDTO> GetCurrentUser(string Token, CancellationToken Cancel);
        Task<ICollection<Achievement>> GetCurrentUserAchievements(string Token, CancellationToken Cancel);
    }
}
