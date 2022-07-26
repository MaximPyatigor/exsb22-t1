using MediatR;

namespace BudgetManager.CQRS.Commands.UserCommands
{
    public record DeleteUserCommand(Guid id) : IRequest<bool>;
}
