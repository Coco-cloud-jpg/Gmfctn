using AutoMapper;
using Data_.Dtos;
using Data_.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<User, UserUpdateDTO>();
            CreateMap<User, UserReadDTO>().ForMember("Roles", src =>
                    src.MapFrom(opt => ConvertRoles(opt)));
            CreateMap<User, UserReadShortDTO>();
            CreateMap<User, UserWithAchievementsDTO>()
                .ForMember("Roles", src =>
                src.MapFrom(opt => ConvertRoles(opt)))
                .ForMember("Achievements", src => 
                    src.MapFrom(opt => ConvertAchievements(opt)))
                .ForMember("DatesCreation", src =>
                src.MapFrom(opt => ConvertDates(opt)));
        }
        private ICollection<string> ConvertRoles(User User)
        {
            if (User?.UserRoles == null)
                return null;

            var Result = new List<string>();

            foreach (var Item in User?.UserRoles)
            {
                Result.Add(Item.Role.RoleName.ToString());
            }

            return Result;
        }

        private ICollection<string> ConvertDates(User User)
        {
            if (User?.UserAchievements == null)
                return null;

            var Result = new List<string>();

            foreach (var Item in User?.UserAchievements)
            {
                Result.Add(Item.AddedTime.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            }

            return Result;
        }


        private ICollection<Achievement> ConvertAchievements(User User)
        {
            if (User?.UserAchievements == null)
            {
                return null;
            }

            List<Achievement> Achievements = new List<Achievement>();
            foreach (var UserAchievement in User.UserAchievements)
            {
                UserAchievement.Achievement.UserAchievements = null;
                Achievements.Add(UserAchievement.Achievement);
            }

            return Achievements;
        }
    }
}
