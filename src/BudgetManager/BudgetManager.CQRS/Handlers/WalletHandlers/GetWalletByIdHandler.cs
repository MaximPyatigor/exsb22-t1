using AutoMapper;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class GetWalletByIdHandler : IRequestHandler<GetWalletByIdQuery, WalletResponse>
    {
        private readonly IBaseRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetWalletByIdHandler(IBaseRepository<User> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<WalletResponse> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = request.userId;
            var walletId = request.walletId;

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId)
                & Builders<User>.Filter.ElemMatch(u => u.Wallets, c => c.Id == walletId && c.IsActive == true);
            var projection = Builders<User>.Projection.Include(u => u.Wallets)
                .ElemMatch(u => u.Wallets, c => c.Id == walletId);

            var response = await _repository.FilterByAsync<User>(filter, projection, cancellationToken);
            var user = response.FirstOrDefault();

            if (user == null) { return null; }

            var usersWallet = user.Wallets.FirstOrDefault();
            if (usersWallet == null) { throw new KeyNotFoundException("Wallet not found"); }
            var mappedWallet = _mapper.Map<WalletResponse>(usersWallet);
            return mappedWallet;
        }
    }
}
