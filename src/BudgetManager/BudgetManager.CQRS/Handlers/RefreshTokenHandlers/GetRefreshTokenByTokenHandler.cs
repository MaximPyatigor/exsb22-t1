using BudgetManager.CQRS.Queries.RefreshTokenQueries;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using BudgetManager.Shared.Utils.Helpers;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.RefreshTokenHandlers
{
    public class GetRefreshTokenByTokenHandler : IRequestHandler<GetRefreshTokenByTokenQuery, RefreshToken>
    {
        private readonly IBaseRepository<RefreshToken> _refreshTokenRepository;

        public GetRefreshTokenByTokenHandler(IBaseRepository<RefreshToken> refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> Handle(GetRefreshTokenByTokenQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<RefreshToken>.Filter.Eq(t => t.Token, request.token);

            var response = (await _refreshTokenRepository.FilterBy(filter, cancellationToken)).FirstOrDefault();

            if (response == null) { throw new AppException("Refresh Token is not found"); }

            return response;
        }
    }
}
