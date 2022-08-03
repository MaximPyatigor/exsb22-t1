using BudgetManager.Model.AuthorizationModels;

namespace BudgetManager.Authorization
{
    public interface IAuthorizationManager
    {
        Task<bool> Register(string email, string password, Guid userId);
        Task<ApplicationUser> Login(string email, string password);
        Task<IEnumerable<ApplicationUser>> GetApplicationUsersList();
    }
}
