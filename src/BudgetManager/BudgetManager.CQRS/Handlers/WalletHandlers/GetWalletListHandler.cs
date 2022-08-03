using AutoMapper;
using BudgetManager.CQRS.Queries.WalletQueries;
using BudgetManager.CQRS.Responses.WalletResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.WalletHandlers
{
    public class GetWalletListHandler : IRequestHandler<GetWalletListQuery, IEnumerable<WalletResponse>>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetWalletListHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WalletResponse>> Handle(GetWalletListQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, request.userId);
            var projection = Builders<User>.Projection.Exclude(u => u.Id).Include(u => u.DefaultWallet).Include(u => u.Wallets);
            var response = await _userRepository.FilterBy<User>(filter, projection, cancellationToken);
            var user = response.FirstOrDefault();

            if (user == null) { throw new KeyNotFoundException("UserId not found"); }

            var usersWallets = user.Wallets;
            var listOfResponseWallets = _mapper.Map<IEnumerable<WalletResponse>>(usersWallets);

            return listOfResponseWallets;
        }
    }
}
