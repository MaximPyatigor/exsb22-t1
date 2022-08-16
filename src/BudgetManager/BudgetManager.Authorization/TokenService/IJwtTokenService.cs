using BudgetManager.Model;
using BudgetManager.Model.AuthorizationModels;
using System.Security.Claims;

namespace BudgetManager.Authorization.TokenService
{
    public interface IJwtTokenService
    {
        string CreateUserToken(ApplicationUser user, IList<string> userRoles);
        Task<RefreshToken> CreateRefreshToken(Guid userId);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
