using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data_.Dtos;
using Data_.Entities;

namespace Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventReadDTO>> GetAllEvents(CancellationToken Cancel);
    }
}
