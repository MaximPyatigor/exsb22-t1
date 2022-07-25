using AutoMapper;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.SDK.DTO.CategoryDTOs;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<ItemDto, Item>();
            CreateMap<AddCategoryDTO, Category>();
            CreateMap<Category, CategoryResponse>();
        }
    }
}
