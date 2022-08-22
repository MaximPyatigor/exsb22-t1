using AutoMapper;
using BudgetManager.CQRS.Queries.PiggyBankQueries;
using BudgetManager.CQRS.Responses.PiggyBankResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using MongoDB.Driver;

namespace BudgetManager.CQRS.Handlers.PiggyBankHandlers
{
    public class GetPiggyBankListHandler : IRequestHandler<GetPiggyBankListQuery, IEnumerable<PiggyBankResponse>>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetPiggyBankListHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PiggyBankResponse>> Handle(GetPiggyBankListQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, request.userId);
            var projection = Builders<User>.Projection.Include(u => u.PiggyBanks);
            var response = await _userRepository.FilterByAsync<User>(filter, projection, cancellationToken);
            var user = response.FirstOrDefault();

            if (user == null) { throw new KeyNotFoundException("User not found"); }
            if (user.PiggyBanks == null) { throw new KeyNotFoundException("Piggy Banks not found"); }

            var piggyBanks = user.PiggyBanks;

            var piggyBankList = _mapper.Map<IEnumerable<PiggyBankResponse>>(piggyBanks);
            return piggyBankList;
        }
    }
}
