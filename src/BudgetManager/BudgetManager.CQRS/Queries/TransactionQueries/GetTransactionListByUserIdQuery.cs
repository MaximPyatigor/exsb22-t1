namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    using BudgetManager.CQRS.Responses.TransactionResponses;
    using MediatR;

    public record GetTransactionListByUserIdQuery(Guid userId, string operationType) : IRequest<IEnumerable<TransactionResponse>>;
}
