using AutoMapper;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.CQRS.Queries.CurrencyQueries;
using BudgetManager.CQRS.Queries.TransactionQueries;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.CQRS.Queries.WalletQueries;
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
        private readonly IMediator _mediator;
        private readonly IBaseRepository<User> _dataAccess;

        public UpdateWalletHandler(IBaseRepository<User> dataAccess, IMapper mapper, IMediator mediator)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<WalletResponse> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var walletId = request.WalletDTO.Id;
            var updateWallet = _mapper.Map<Wallet>(request.WalletDTO);

            updateWallet.Currency = await _mediator.Send(new GetCurrencyByIdQuery(request.WalletDTO.CurrencyId), cancellationToken);

            var walletFilter = Builders<Wallet>.Filter.Eq(w => w.Id, updateWallet.Id);
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId)
                & Builders<User>.Filter.ElemMatch(u => u.Wallets, walletFilter);

            // Used to add update definitions.
            var updList = new List<UpdateDefinition<User>>();

            // If the wallet already has some transactions, currency can not be changed.
            var defaultUpdate = Builders<User>.Update
                    .Set(u => u.Wallets[-1].Name, updateWallet.Name)
                    .Set(u => u.Wallets[-1].Balance, updateWallet.Balance)
                    .Set(u => u.Wallets[-1].DateOfChange, updateWallet.DateOfChange);

            updList.Add(defaultUpdate);

            var transactions = await _mediator.Send(new GetTransactionListByWalletQuery(walletId), cancellationToken);
            if (transactions == null || !transactions.Any())
            {
                var updateWalletCurrency = Builders<User>.Update
                    .Set(u => u.Wallets[-1].Currency, updateWallet.Currency);

                updList.Add(updateWalletCurrency);
            }

            if(request.isDefault)
            {
                var defaultWalletUpdate = Builders<User>.Update
                    .Set(u => u.DefaultWallet, walletId);

                updList.Add(defaultWalletUpdate);
            }

            var finalUpdate = Builders<User>.Update.Combine(updList);
            var result = await _dataAccess.UpdateOneAsync(filter, finalUpdate, cancellationToken);
            if(result is null)
            {
                throw new KeyNotFoundException("WalletId not found");
            }

            // We return this instead of updateWallet in case updateWallet tried to change currency when it was not allowed.
            var updatedWallet = result.Wallets.Where(w => w.Id == walletId).FirstOrDefault();
            return _mapper.Map<WalletResponse>(updatedWallet);
        }
    }
}
