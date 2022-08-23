using AutoMapper;
using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class AddExpenseTransactionHandler : IRequestHandler<AddExpenseTransactionCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _dataAccess;

        public AddExpenseTransactionHandler(IMapper mapper, IMediator mediator, ITransactionRepository dataAccess)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<Guid> Handle(AddExpenseTransactionCommand request, CancellationToken cancellationToken)
        {
            var expenseTransaction = _mapper.Map<Transaction>(request.addExpenseDTO);
            expenseTransaction.UserId = request.userId;
            await _dataAccess.InsertOneAsync(expenseTransaction, cancellationToken);

            await _mediator.Send(new ChangeTotalBalanceOfWalletCommand(expenseTransaction), cancellationToken);

            await _mediator.Send(new UpdateWalletDateOfChangeCommand(expenseTransaction.UserId, expenseTransaction.WalletId, DateTime.UtcNow), cancellationToken);

            return expenseTransaction.Id;
        }
    }
}
