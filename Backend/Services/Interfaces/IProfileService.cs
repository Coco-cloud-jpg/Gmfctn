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
        Task<UserWithAchievementsDTO> GetCurrentUser(string Token, CancellationToken Cancel);
        Task<ICollection<Achievement>> GetCurrentUserAchievements(string Token, CancellationToken Cancel);
        Task UpdateCurrentUser(string Token, UserUpdateDTO NewUser, CancellationToken Cancel);
        Task ChangePassword(string Token, string OldPassword, string NewPassword, CancellationToken Cancel);

    }
}
