using AutoMapper;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class AddWalletHandler : IRequestHandler<AddWalletCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<User> _dataAccess;

        public AddWalletHandler(IMapper mapper ,IBaseRepository<User> dataAccess)
        {
            _mapper = mapper;
            _dataAccess = dataAccess;
        }

        public async Task<Guid> Handle(AddWalletCommand request, CancellationToken cancellationToken)
        {
            var userId = request.addWalletDTO.UserId;
            var setDefault = request.addWalletDTO.SetDefault;
            var wallet = _mapper.Map<Wallet>(request.addWalletDTO);

            wallet.DateOfChange = DateTime.UtcNow;

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Push(u => u.Wallets, wallet);

            if (setDefault)
            {
                update.Set(u => u.DefaultWallet, wallet.Id);
            }

            var result = await _dataAccess.UpdateOneAsync(filter, update, cancellationToken);

            if (result is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (result.Wallets.Count == 1)
            {
                var updateDefaultWallet = Builders<User>.Update.Set(u => u.DefaultWallet, wallet.Id);
                result = await _dataAccess.UpdateOneAsync(filter, updateDefaultWallet, cancellationToken);
            }

            return wallet.Id;
        }
    }
}
