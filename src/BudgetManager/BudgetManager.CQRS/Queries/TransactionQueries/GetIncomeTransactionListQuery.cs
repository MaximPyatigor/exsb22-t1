using BudgetManager.CQRS.Responses.TransactionResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Queries.TransactionQueries
{
    public record GetIncomeTransactionListQuery(Guid userId, IncomesPageDTO incomesPageDto) : IRequest<IncomePageResponse>;
}
