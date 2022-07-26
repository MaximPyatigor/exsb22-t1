using AutoMapper;
using BudgetManager.CQRS.Responses.NotificationResponses;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<UpdateTransactionDTO, Transaction>();
            CreateMap<Notification, NotificationResponse>();
        }
    }
}
