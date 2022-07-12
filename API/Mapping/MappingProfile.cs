using AutoMapper;
using API.Models;
using Infrastructure.Models;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, ApplicationUser>().ForMember("UserName", opt => opt.MapFrom(c => c.Email));
        }
    }
}
