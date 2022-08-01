using AutoMapper;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class AddWalletHandler : IRequestHandler<AddWalletCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IWalletRepository _dataAccess;

        public AddWalletHandler(IMapper mapper, IWalletRepository dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<Guid> Handle(AddWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = _mapper.Map<Wallet>(request.walletDTO);
            await _dataAccess.InsertOneAsync(wallet, cancellationToken);

            return wallet.Id;
        }
    }
}
