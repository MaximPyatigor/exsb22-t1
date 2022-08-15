using BudgetManager.CQRS.Commands.RefreshTokenCommands;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.RefreshTokenHandlers
{
    public class AddRefreshTokenHandler : IRequestHandler<AddRefreshTokenCommand, Guid>
    {
        private readonly IBaseRepository<RefreshToken> _refreshTokenRepository;

        public AddRefreshTokenHandler(IBaseRepository<RefreshToken> refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Guid> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = request.refreshToken;

            await _refreshTokenRepository.InsertOneAsync(token, cancellationToken);

            return token.Id;
        }
    }
}
