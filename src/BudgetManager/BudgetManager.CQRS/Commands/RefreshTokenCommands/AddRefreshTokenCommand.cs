using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Commands.RefreshTokenCommands
{
    public record AddRefreshTokenCommand(RefreshToken refreshToken) : IRequest<Guid>;
}
