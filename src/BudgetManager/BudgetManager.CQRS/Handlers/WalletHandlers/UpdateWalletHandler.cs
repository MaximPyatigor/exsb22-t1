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
        private readonly IBaseRepository<User> _dataAccess;

        public UpdateWalletHandler(IBaseRepository<User> dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async Task<WalletResponse> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var updateWallet = _mapper.Map<Wallet>(request.WalletDTO);
            var userId = request.UserId;

            var walletFilter = Builders<Wallet>.Filter.Eq(w => w.Id, updateWallet.Id);
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId)
                & Builders<User>.Filter.ElemMatch(u => u.Wallets, walletFilter);

            var update = Builders<User>.Update
                .Set(u => u.Wallets[-1].Name, updateWallet.Name)
                .Set(u => u.Wallets[-1].Currency, updateWallet.Currency)
                .Set(u => u.Wallets[-1].Balance, updateWallet.Balance)
                .Set(u => u.Wallets[-1].DateOfChange, updateWallet.DateOfChange);

            var result = await _dataAccess.UpdateOneAsync(filter, update, cancellationToken);
            return result is null ? null : _mapper.Map<WalletResponse>(updateWallet);
        }
    }
}
