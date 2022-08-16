using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Commands.RefreshTokenCommands
{
    public record UpdateRefreshTokenCommand(RefreshToken token) : IRequest<RefreshToken>;
}
