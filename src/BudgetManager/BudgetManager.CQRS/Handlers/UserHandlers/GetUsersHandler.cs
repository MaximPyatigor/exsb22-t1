using AutoMapper;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.Model;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUsersHandler(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var allUsers = _mapper.Map<List<UserResponse>>(await _userRepository.GetAllAsync(cancellationToken));
            return allUsers;
        }
    }
}
