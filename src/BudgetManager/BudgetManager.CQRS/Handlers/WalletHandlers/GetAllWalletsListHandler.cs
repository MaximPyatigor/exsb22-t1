using AutoMapper;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class GetAllWalletsListHandler : IRequestHandler<GetAllWalletsListQuery, IEnumerable<WalletResponse>>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetAllWalletsListHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WalletResponse>> Handle(GetAllWalletsListQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, request.userId);
            var projection = Builders<User>.Projection.Exclude(u => u.Id).Include(u => u.DefaultWallet).Include(u => u.Wallets);
            var response = await _userRepository.FilterBy<User>(filter, projection, cancellationToken);
            var user = response.FirstOrDefault();

            if (user == null) { throw new KeyNotFoundException("User not found"); }
            if (user.Wallets == null) { throw new KeyNotFoundException("Wallets not found"); }

            var userWallets = user.Wallets.OrderByDescending(w => w.DateOfChange);

            if (user.DefaultWallet is not null) { userWallets = userWallets.OrderBy(w => w.Id != user.DefaultWallet); }

            var listOfResponseWallets = _mapper.Map<IEnumerable<WalletResponse>>(userWallets);
            return listOfResponseWallets;
        }
    }
}
