using AutoMapper;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;
using BudgetManager.SDK.DTO.UserDTOs;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddUserDTO, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<Wallet, WalletResponse>();
            CreateMap<AddWalletDTO, Wallet>();
            CreateMap<UpdateWalletDTO, Wallet>(); 
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<AddTransactionDTO, Transaction>();
            CreateMap<UpdateTransactionDTO, Transaction>();
            CreateMap<Notification, NotificationResponse>();
        }
    }
}
