using AutoMapper;
using API.Models;
using Domain.Models;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, ApplicationUser>().ReverseMap();
        }
    }
}
