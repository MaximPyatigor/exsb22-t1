using BudgetManager.Model.AuthorizationModels;

namespace BudgetManager.Authorization
{
    public interface IAuthorizationManager
    {
        Task<bool> Register(string email, string password, Guid userId, bool isAdmin);
        Task<AuthorizationResponse> Login(string email, string password);
        Task<RefreshedTokenResponse> RefreshToken(RefreshedTokenResponse tokenResponse);
        Task<IEnumerable<ApplicationUser>> GetApplicationUsersList();
    }
}
