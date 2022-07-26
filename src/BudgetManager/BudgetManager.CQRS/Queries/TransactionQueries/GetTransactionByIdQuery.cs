using BudgetManager.CQRS.Responses.TransactionResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetTransactionByIdQuery(Guid id) : IRequest<TransactionResponse>;
}