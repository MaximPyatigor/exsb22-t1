using AutoMapper;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class UpdateWalletHandler : IRequestHandler<UpdateWalletCommand, WalletResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Wallet> _dataAccess;

        public UpdateWalletHandler(IBaseRepository<Wallet> dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<WalletResponse> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var updateWallet = _mapper.Map<Wallet>(request.walletDTO);
            var filter = Builders<Wallet>.Filter.Eq(opt => opt.Id, updateWallet.Id);
            var update = Builders<Wallet>.Update.Set(o => o.Name, updateWallet.Name).Set(o => o.Currency, updateWallet.Currency);
            var result = _mapper.Map<WalletResponse>(await _dataAccess.UpdateOneAsync(filter, update, cancellationToken));

            return result;
        }
    }
}
