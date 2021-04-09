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
        }
    }
}
