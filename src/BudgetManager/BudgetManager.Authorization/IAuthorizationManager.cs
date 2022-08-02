using BudgetManager.Model.AuthorizationModels;

namespace BudgetManager.Authorization
{
    public interface IAuthorizationManager
    {
        Task<bool> Register(string email, string password);
        Task<ApplicationUser> Login(string email, string password);
    }
}
