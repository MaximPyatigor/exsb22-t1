using MediatR;

namespace BudgetManager.CQRS.Queries.UserQueries
{
    public record GetUserPayersQuery(Guid userId) : IRequest<IEnumerable<string>?>;
}
