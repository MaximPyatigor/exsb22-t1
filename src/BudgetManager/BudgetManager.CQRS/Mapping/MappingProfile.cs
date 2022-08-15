using AutoMapper;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.CQRS.Responses.CurrencyResponses;
using BudgetManager.SDK.DTOs;
using BudgetManager.CQRS.Responses.CountryResponses;
using BudgetManager.CQRS.Responses.DefaultCategoryResponses;
using BudgetManager.Model.Enums;
using BudgetManager.CQRS.Responses.SubCategoryResponses;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();
            CreateMap<AddSubCategoryDTO, Category>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<AddUserDTO, User>()
                .ForMember(o => o.Payers, p => p.MapFrom(b => new List<string>() { "Me" }));
            CreateMap<User, UserResponse>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<Wallet, WalletResponse>();
            CreateMap<AddWalletDTO, Wallet>();
            CreateMap<UpdateWalletDTO, Wallet>(); 
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<UpdateTransactionDTO, Transaction>();
            CreateMap<AddNotificationDto, Notification>()
                .ForMember(o => o.Date, p => p.MapFrom(b => DateTime.UtcNow));
            CreateMap<Notification, NotificationResponse>();
            CreateMap<Country, CountryResponse>();
            CreateMap<DefaultCategory, DefaultCategoryResponse>()
                .ForMember(o => o.CategoryType, p => p.MapFrom(b => b.CategoryType));
            CreateMap<DefaultCategory, Category>();
            CreateMap<Currency, CurrencyResponse>();
            CreateMap<AddExpenseTransactionDTO, Transaction>()
                .ForMember(o => o.TransactionType, p => p.MapFrom(b => OperationType.Expense));
            CreateMap<Transaction, ExpenseTransactionResponse>();
            CreateMap<AddIncomeTransactionDTO, Transaction>()
                .ForMember(o => o.TransactionType, p => p.MapFrom(b => OperationType.Income));
            CreateMap<Transaction, IncomeTransactionResponse>();
            CreateMap<Category, SubCategoryResponse>();
            CreateMap<Transaction, RecentTransactionResponse>()
                .ForMember(o => o.Value, p => p.MapFrom(b => b.TransactionType == OperationType.Income ? b.Value : -b.Value))
                .ForMember(o => o.DateOfTransaction, p => p.MapFrom(b => b.DateOfTransaction.Date));
            CreateMap<CategoryResponse, WalletCategoryResponse>();
        }
    }
}
