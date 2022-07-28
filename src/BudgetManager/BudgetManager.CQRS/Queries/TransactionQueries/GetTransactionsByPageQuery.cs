using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetTransactionsByPageQuery(int pageNumber) : IRequest<IEnumerable<Transaction>>;
}
