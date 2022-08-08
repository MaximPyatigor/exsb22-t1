using BudgetManager.CQRS.Responses.TransactionResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetIncomeTransactionListQuery(Guid userId) : IRequest<IEnumerable<IncomeTransactionResponse>>;
}
