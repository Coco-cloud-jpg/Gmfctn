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
        }
        private ICollection<string> ConvertRoles(User User)
        {
            var Result = new List<string>();
            foreach (var Item in User?.UserRoles)
            {
                Result.Add(Item.Role.RoleName.ToString());
            }
            return Result;
        }
    }
}
