namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    using BudgetManager.CQRS.Responses.TransactionResponses;
    using BudgetManager.Model.Enums;
    using MediatR;

    public record GetTransactionListByOperationQuery(Guid userId, OperationType operationType) : IRequest<IEnumerable<TransactionResponse>>;
}
