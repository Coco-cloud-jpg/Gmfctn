using Data_.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data_.Dtos;

namespace Services.Interfaces
{
    public interface IThankService
    {
        Task SayThank(string Token,string Text, Guid ToUserId, CancellationToken Cancel);
        Task<ThankReadDTO> GetThank(string Token, CancellationToken Cancel);
    }
}
