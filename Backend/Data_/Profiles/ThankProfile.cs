using AutoMapper;
using Data_.Entities;
using Data_.Dtos;

namespace Data_.Profiles
{
    public class ThankProfile : Profile
    {
        public ThankProfile()
        {
            CreateMap<Thank, ThankReadDTO>().ForMember("AddedTime", src => src.MapFrom(opt => opt.AddedTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")));
        }

    }
}
