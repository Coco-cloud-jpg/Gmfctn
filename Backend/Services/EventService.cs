using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data_.Dtos;
using Data_.Entities;
using Data_.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services
{
    public class EventService: IEventService
    {
        private IUnitOfWork UnitOfWork;
        private IMapper Mapper;
        public EventService(IUnitOfWork _UnitOfWork, IMapper _Mapper)
        {
            UnitOfWork = _UnitOfWork;
            Mapper = _Mapper;
        }
        public async Task<IEnumerable<EventReadDTO>> GetAllEvents(CancellationToken Cancel)
        {
            var Events = await UnitOfWork.EventRepository.DbSet.Include(Event => Event.User).ToListAsync(Cancel);
            Events.ForEach(Event => Event.User.Events = null);
            return Mapper.Map<IEnumerable<EventReadDTO>>(Events);
        }
    }
}
