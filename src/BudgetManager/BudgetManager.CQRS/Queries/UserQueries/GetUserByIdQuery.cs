using BudgetManager.CQRS.Responses.UserResponses;
using MediatR;

namespace BudgetManager.CQRS.Queries.UserQueries
{
    public record GetUserByIdQuery(Guid id) : IRequest<UserResponse>;
}
