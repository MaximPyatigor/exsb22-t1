using AutoMapper;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.Model;
using BudgetManager.SDK.DTOs;

namespace BudgetManager.CQRS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Wallet, WalletResponse>();
            CreateMap<AddWalletDTO, Wallet>();
            CreateMap<UpdateWalletDTO, Wallet>();
        }
    }
}
