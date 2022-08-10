using AutoMapper;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.CQRS.Queries.CurrencyQueries;
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
            var updateWallet = _mapper.Map<Wallet>(request.WalletDTO);
            var userId = request.UserId;

            updateWallet.Currency = await _mediator.Send(new GetCurrencyByIdQuery(request.WalletDTO.CurrencyId), cancellationToken);
            Console.WriteLine(updateWallet.Currency.Id);

            var walletFilter = Builders<Wallet>.Filter.Eq(w => w.Id, updateWallet.Id);
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId)
                & Builders<User>.Filter.ElemMatch(u => u.Wallets, walletFilter);

            var update = Builders<User>.Update
                .Set(u => u.Wallets[-1].Name, updateWallet.Name)
                .Set(u => u.Wallets[-1].Currency, updateWallet.Currency)
                .Set(u => u.Wallets[-1].Balance, updateWallet.Balance)
                .Set(u => u.Wallets[-1].DateOfChange, updateWallet.DateOfChange);

            var result = await _dataAccess.UpdateOneAsync(filter, update, cancellationToken);
            if(result is null)
            {
                throw new KeyNotFoundException("WalletId not found");
            }

            return _mapper.Map<WalletResponse>(updateWallet);
        }
    }
}
