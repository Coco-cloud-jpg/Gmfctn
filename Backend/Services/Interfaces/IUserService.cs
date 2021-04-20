using Data_.Dtos;
using Data_.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<UserReadDTO> GetUserById(Guid Id, CancellationToken Cancel);
        Task<IEnumerable<UserReadDTO>> GetAllUsers(CancellationToken Cancel);
        Task<IEnumerable<UserReadShortDTO>> GetAllUsersInfo(CancellationToken Cancel);
        Task<UserReadShortDTO> GetUserInfoById(Guid Id, CancellationToken Cancel);
        Task DeleteUser(Guid Id, CancellationToken Cancel);
        Task UpdateUser(Guid Id, UserUpdateDTO User, CancellationToken Cancel);
        Task CreateUser(UserCreateDTO User, CancellationToken Cancel);
    }
}
