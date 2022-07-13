using Domain.Models;
using Infrastructure.Models;

namespace Application.JwtTokenService
{
    public interface IJwtTokenService
    {
        string CreateUserToken(ApplicationUser user,  IList<string> userRoles);
    }
}