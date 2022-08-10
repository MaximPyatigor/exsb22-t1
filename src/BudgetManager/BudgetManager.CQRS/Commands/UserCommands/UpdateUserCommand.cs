using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.UserCommands
{
    public record UpdateUserCommand(Guid userId, UpdateUserDTO updateUser) : IRequest<UserResponse>;
}
