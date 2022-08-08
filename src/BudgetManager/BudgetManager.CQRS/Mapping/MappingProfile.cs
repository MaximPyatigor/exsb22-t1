﻿using AutoMapper;
using BudgetManager.CQRS.Responses.CategoryResponses;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs.CategoryDTOs;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.CQRS.Responses.CurrencyResponses;
using BudgetManager.SDK.DTOs;
using BudgetManager.CQRS.Responses.CountryResponses;
using BudgetManager.CQRS.Responses.DefaultCategoryResponses;
using BudgetManager.Model.Enums;

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
            CreateMap<AddUserDTO, User>()
                .ForMember(o => o.Payers, p => p.MapFrom(b => new List<string>() { b.FullName }));
            CreateMap<User, UserResponse>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<Wallet, WalletResponse>();
            CreateMap<AddWalletDTO, Wallet>();
            CreateMap<UpdateWalletDTO, Wallet>(); 
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<UpdateTransactionDTO, Transaction>();
            CreateMap<AddNotificationDto, Notification>();
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
        }
    }
}
