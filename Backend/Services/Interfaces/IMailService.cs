using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMailService
    {
        Task SendRequest(string Email, CancellationToken Cancel);
    }
}
