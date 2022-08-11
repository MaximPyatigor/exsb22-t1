using MediatR;

namespace BudgetManager.CQRS.Queries.UserQueries
{
    public record CalculateUserTotalBalanceQuery(Guid userId, string currencyCode) : IRequest<decimal>;
}
