using AutoMapper;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.SDK.DTO.CategoryDTOs;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.SDK.DTOs;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<ItemDto, Item>();
            CreateMap<AddCategoryDTO, Category>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<UpdateCategoryDTO, Category>();
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<UpdateTransactionDTO, Transaction>();
            CreateMap<Notification, NotificationResponse>();
        }
    }
}
