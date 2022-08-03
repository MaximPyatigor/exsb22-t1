using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.Model;
using BudgetManager.Model.AuthorizationModels;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BudgetManager.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthorizationManager(UserManager<ApplicationUser> userManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<bool> Register(string email, string password, Guid userId)
        {
            var appUser = new ApplicationUser { UserName = email,
                Email = email,
                UserId = userId };

            var savedAppUser = await _userManager.CreateAsync(appUser, password);

            if (savedAppUser.Succeeded)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<ApplicationUser> Login(string email, string password)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            var user = await _mediator.Send(new GetUserByIdQuery(appUser.Id));

            if (appUser != null & await _userManager.CheckPasswordAsync(appUser, password)
                & user != null)
            {
                return appUser;
            } else
            {
                return null;
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersList()
        {
            return await Task.Run(() => _userManager.Users.ToList());
        }
    }
}
