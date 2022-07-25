using BudgetManager.CQRS.Responses.TransactionResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetTransactionListQuery() : IRequest<IEnumerable<TransactionResponse>>;
}