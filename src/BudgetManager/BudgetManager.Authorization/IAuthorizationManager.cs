using BudgetManager.Model.AuthorizationModels;

namespace BudgetManager.Authorization
{
    public interface IAuthorizationManager
    {
        Task<bool> RegisterAsync(string email, string password, Guid userId, bool isAdmin);
        Task<AuthorizationResponse> LoginAsync(string email, string password);
        Task<IEnumerable<ApplicationUser>> GetApplicationUsersListAsync();
    }
}
