using BudgetManager.Model;
using BudgetManager.Model.AuthorizationModels;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;
using Microsoft.AspNetCore.Identity;

namespace BudgetManager.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthorizationManager(IBaseRepository<User> userRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<bool> Register(string email, string password)
        {
            var appUser = new ApplicationUser { UserName = email, Email = email };

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

            if (appUser != null & await _userManager.CheckPasswordAsync(appUser, password))
            {
                return appUser;
            } else
            {
                return null;
            }
        }
    }
}
