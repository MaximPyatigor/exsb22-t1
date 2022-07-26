using BudgetManager.SDK.DTOs;
using MediatR;

namespace BudgetManager.CQRS.Commands.UserCommands
{
    public record AddUserCommand(AddUserDTO user) : IRequest<Guid>;
}
