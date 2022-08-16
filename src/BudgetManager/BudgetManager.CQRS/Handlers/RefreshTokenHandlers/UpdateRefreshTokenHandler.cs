using MediatR;
using BudgetManager.CQRS.Commands.RefreshTokenCommands;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Model;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.RefreshTokenHandlers
{
    public class UpdateRefreshTokenHandler : IRequestHandler<UpdateRefreshTokenCommand, RefreshToken>
    {
        private readonly IBaseRepository<RefreshToken> _refreshTokenRepository;

        public UpdateRefreshTokenHandler(IBaseRepository<RefreshToken> refreshTokenRepo)
        {
            _refreshTokenRepository = refreshTokenRepo;
        }

        public async Task<RefreshToken> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var updateToken = request.token;

            var filter = Builders<RefreshToken>.Filter.Eq(t => t.Id, updateToken.Id);
            var update = Builders<RefreshToken>.Update.Set(t => t.Token, updateToken.Token);

            var res = await _refreshTokenRepository.UpdateOneAsync(filter, update, cancellationToken);

            return res;
        }
    }
}
