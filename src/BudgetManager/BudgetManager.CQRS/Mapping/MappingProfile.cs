using AutoMapper;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.Model;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Wallet, WalletResponse>();
        }
    }
}
