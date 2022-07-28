using AutoMapper;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs.CategoryDTOs;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.SDK.DTOs;
using BudgetManager.CQRS.Responses.CountryResponses;
using BudgetManager.CQRS.Responses.DefaultCategoryResponses;

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
            CreateMap<AddUserDTO, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<Wallet, WalletResponse>();
            CreateMap<AddWalletDTO, Wallet>();
            CreateMap<UpdateWalletDTO, Wallet>(); 
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<AddTransactionDTO, Transaction>();
            CreateMap<UpdateTransactionDTO, Transaction>();
            CreateMap<AddNotificationDto, Notification>();
            CreateMap<Notification, NotificationResponse>();
            CreateMap<Country, CountryResponse>();
            CreateMap<DefaultCategory, DefaultCategoryResponse>();
        }
    }
}
