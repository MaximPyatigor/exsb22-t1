using AutoMapper;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.Model;
using BudgetManager.SDK.DTO.UserDTOs;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<ItemDto, Item>();
            CreateMap<AddUserDTO, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UpdateUserDTO, User>();
        }
    }
}
