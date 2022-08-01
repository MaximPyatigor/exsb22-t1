using AutoMapper;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.DataAccess.MongoDbAccess.Interfaces;
using MediatR;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<UserResponse>(await _userRepository.FindByIdAsync(request.id, cancellationToken));
            return result;
        }
    }
}
