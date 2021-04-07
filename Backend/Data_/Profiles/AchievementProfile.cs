using AutoMapper;
using Data_.Dtos;
using System;

namespace Data_.Profiles
{
    public class AchievementProfile : Profile
    {
        public AchievementProfile()
        {
            CreateMap<AchievementCreateDTO, Achievement>()
                .ForMember("Xp", opt => opt.MapFrom(src => src.Xp<=0? 1: src.Xp));
            CreateMap<AchievementUpdateDTO, Achievement>();
        }
        
    }
}
