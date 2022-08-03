using MediatR;

namespace BudgetManager.CQRS.Commands.UserCommands
{
    public record AddUserPayerCommand(Guid userId, string name) : IRequest<string?>;
}
