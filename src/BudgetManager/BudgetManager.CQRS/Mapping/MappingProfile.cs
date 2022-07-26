using AutoMapper;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.Model;
using BudgetManager.SDK.DTO;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionResponse>();
            CreateMap<UpdateTransactionDTO, Transaction>();
        }
    }
}
