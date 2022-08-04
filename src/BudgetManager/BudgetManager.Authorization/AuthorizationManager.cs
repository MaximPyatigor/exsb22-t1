using BudgetManager.Authorization.TokenService;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.Model.AuthorizationModels;
using BudgetManager.Shared.Utils.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BudgetManager.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _tokenService;

        public AuthorizationManager(UserManager<ApplicationUser> userManager,
            IMediator mediator,
            IJwtTokenService tokenService)
        {
            _userManager = userManager;
            _mediator = mediator;
            _tokenService = tokenService;
        }

        public async Task<bool> Register(string email, string password, Guid userId,
            bool isAdmin)
        {
            var appUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
                UserId = userId
            };

            var savedAppUser = await _userManager.CreateAsync(appUser, password);

            if (isAdmin)
            {
                await _userManager.AddToRolesAsync(appUser, new string[] { "Admin", "User" });
            }
            else
            {
                await _userManager.AddToRoleAsync(appUser, "User");
            }

            if (savedAppUser.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<AuthorizationResponse> Login(string email, string password)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            var user = await _mediator.Send(new GetUserByIdQuery(appUser.UserId));

            if (appUser != null & await _userManager.CheckPasswordAsync(appUser, password)
                & user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(appUser);

                var token = _tokenService.CreateUserToken(appUser, userRoles);

                var response = new AuthorizationResponse
                {
                    User = user,
                    Token = token
                };
                return response;
            }
            else
            {
                throw new KeyNotFoundException("User not found or password is not correct");
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersList()
        {
            return await Task.Run(() => _userManager.Users.ToList());
        }
    }
}
