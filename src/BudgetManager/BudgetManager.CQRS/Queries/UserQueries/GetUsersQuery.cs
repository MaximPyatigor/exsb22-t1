using BudgetManager.CQRS.Responses.UserResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.UserQueries
{
    public record GetUsersQuery : IRequest<List<UserResponse>>;
}
