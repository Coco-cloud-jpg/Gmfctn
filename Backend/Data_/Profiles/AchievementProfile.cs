using AutoMapper;
using Data_.Dtos;
using System;

namespace Data_.Profiles
{
    public class AchievementProfile : Profile
    {
        public AchievementProfile()
        {
            CreateMap<AchievementCreateDTO, Achievement>();
            CreateMap<AchievementCreateDTO, AchievementUpdateDTO>();
            CreateMap<AchievementUpdateDTO, AchievementCreateDTO>();
            CreateMap<AchievementUpdateDTO, Achievement>();
        }
        
    }
}
