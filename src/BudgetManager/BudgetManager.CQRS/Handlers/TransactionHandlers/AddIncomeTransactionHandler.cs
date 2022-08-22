using AutoMapper;
using BudgetManager.CQRS.Commands.TransactionCommands;
using BudgetManager.CQRS.Commands.WalletCommands;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Handlers.TransactionHandlers
{
    public class AddIncomeTransactionHandler : IRequestHandler<AddIncomeTransactionCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ITransactionRepository _dataAccess;

        public AddIncomeTransactionHandler(IMapper mapper, IMediator mediator, ITransactionRepository dataAccess)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<Guid> Handle(AddIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var incomeTransaction = _mapper.Map<Transaction>(request.addIncomeDTO);
            incomeTransaction.UserId = request.userId;
            await _dataAccess.InsertOneAsync(incomeTransaction, cancellationToken);

            await _mediator.Send(new ChangeTotalBalanceOfWalletCommand(incomeTransaction), cancellationToken);

            await _mediator.Send(new UpdateWalletDateOfChangeCommand(incomeTransaction.UserId, incomeTransaction.WalletId, DateTimeOffset.Now), cancellationToken);

            return incomeTransaction.Id;
        }
    }
}
