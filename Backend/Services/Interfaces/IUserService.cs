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
        Task<bool> DeleteUser(Guid Id, CancellationToken Cancel);
        Task<bool> UpdateUser(Guid Id, UserUpdateDTO User, CancellationToken Cancel);
        Task<bool> CreateUser(UserCreateDTO User, CancellationToken Cancel);
    }
}
