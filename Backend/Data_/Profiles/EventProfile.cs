using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Data_.Dtos;
using Data_.Entities;

namespace Data_.Profiles
{
    public class EventProfile: Profile
    {
        public EventProfile()
            {
                CreateMap<Event, EventReadDTO>()
                    .ForMember("CreatedTime", src =>
                        src.MapFrom(opt => opt.CreatedTime.ToString("yyyy-MM-ddTHH:mm:ssZ")));
            }

    }
}
