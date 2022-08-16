using BudgetManager.Model;
using MediatR;

namespace BudgetManager.CQRS.Queries.RefreshTokenQueries
{
    public record GetRefreshTokenByTokenQuery(string token) : IRequest<RefreshToken>;
}
