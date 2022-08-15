using BudgetManager.Model;
using BudgetManager.Model.AuthorizationModels;

namespace BudgetManager.Authorization.TokenService
{
    public interface IJwtTokenService
    {
        string CreateUserToken(ApplicationUser user, IList<string> userRoles);
        Task<RefreshToken> CreateRefreshToken(Guid userId);
    }
}
