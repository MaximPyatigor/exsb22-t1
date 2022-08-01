using BudgetManager.CQRS.Responses.TransactionResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetTransactionsByUserIdQuery(Guid userId) : IRequest<IEnumerable<TransactionResponse>>;
}