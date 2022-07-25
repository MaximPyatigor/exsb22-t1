using AutoMapper;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.Model;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, TransactionResponse>();
        }
    }
}
