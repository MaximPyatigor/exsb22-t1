using AutoMapper;
using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class UpdateIncomeTransactionHandler : IRequestHandler<UpdateIncomeTransactionCommand, IncomeTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBaseRepository<User> _userRepository;

        public UpdateIncomeTransactionHandler(IMapper mapper, IMediator mediator, ITransactionRepository transactionRepository, IBaseRepository<User> userRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }

        public async Task<IncomeTransactionResponse> Handle(UpdateIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var transactionFilter = Builders<Transaction>.Filter.And(
                Builders<Transaction>.Filter.Eq(t => t.UserId, request.userId),
                Builders<Transaction>.Filter.Eq(t => t.Id, request.updateIncomeDTO.Id),
                Builders<Transaction>.Filter.Eq(t => t.TransactionType, Model.Enums.OperationType.Income));

            var oldIncomeTransaction = (await _transactionRepository.FilterBy(transactionFilter, cancellationToken))
                                .FirstOrDefault() ?? throw new KeyNotFoundException("Transaction not found.");
            bool walletChanged = oldIncomeTransaction.WalletId != request.updateIncomeDTO.WalletId;

            var user = await _userRepository.FindByIdAsync(request.userId, cancellationToken);
            var wallets = user.Wallets ?? throw new KeyNotFoundException("User has no wallets");
            var updatedWallets = new List<Wallet>();

            if (walletChanged)
            {
                foreach (var wallet in user.Wallets)
                {
                    if (wallet.Id == oldIncomeTransaction.WalletId)
                    {
                        wallet.Balance -= oldIncomeTransaction.Value;
                        wallet.DateOfChange = DateTime.UtcNow;
                    }
                    else if (wallet.Id == request.updateIncomeDTO.WalletId)
                    {
                        wallet.Balance += request.updateIncomeDTO.Value;
                        wallet.DateOfChange = DateTime.UtcNow;
                    }

                    updatedWallets.Add(wallet);
                }
            }
            else
            {
                foreach (var wallet in wallets)
                {
                    if (wallet.Id == request.updateIncomeDTO.WalletId)
                    {
                        wallet.Balance = wallet.Balance - oldIncomeTransaction.Value + request.updateIncomeDTO.Value;
                        wallet.DateOfChange = DateTime.UtcNow;
                    }

                    updatedWallets.Add(wallet);
                }
            }

            var userFilter = Builders<User>.Filter.Eq(u => u.Id, request.userId);
            var userWalletUpdate = Builders<User>.Update.Set(u => u.Wallets, updatedWallets);
            await _userRepository.UpdateOneAsync(userFilter, userWalletUpdate, cancellationToken);

            var update = Builders<Transaction>.Update
                .Set(t => t.WalletId, request.updateIncomeDTO.WalletId)
                .Set(t => t.CategoryId, request.updateIncomeDTO.CategoryId)
                .Set(t => t.DateOfTransaction, request.updateIncomeDTO.DateOfTransaction)
                .Set(t => t.Value, request.updateIncomeDTO.Value)
                .Set(t => t.Description, request.updateIncomeDTO.Description);

            var updatedIncomeTransaction = await _transactionRepository.UpdateOneAsync(transactionFilter, update, cancellationToken);
            if (updatedIncomeTransaction == null) { throw new KeyNotFoundException("Transaction not found"); }

            return _mapper.Map<IncomeTransactionResponse>(updatedIncomeTransaction);
        }
    }
}