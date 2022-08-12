using AutoMapper;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.CQRS.Responses.UserResponses;
using BudgetManager.Model;
using BudgetManager.Model.AuthorizationModels;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BudgetManager.CQRS.Handlers.UserHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserByIdHandler(IBaseRepository<User> userRepository, IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<UserResponse>(await _userRepository.FindByIdAsync(request.id, cancellationToken));

            var appUser = await _userManager.FindByEmailAsync(result.Email);
            if (appUser == null) { throw new ApplicationException("AppUser corresponding to this User not found"); }
            result.IsAdmin = await _userManager.IsInRoleAsync(appUser, "Admin");

            return result;
        }
    }
}
