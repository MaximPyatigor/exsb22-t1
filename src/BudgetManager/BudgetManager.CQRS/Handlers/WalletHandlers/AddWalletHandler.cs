using AutoMapper;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class AddWalletHandler : IRequestHandler<AddWalletCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Wallet> _dataAccess;

        public AddWalletHandler(IMapper mapper ,IBaseRepository<Wallet> dataAccess)
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
