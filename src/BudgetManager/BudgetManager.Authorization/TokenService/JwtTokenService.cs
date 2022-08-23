using BudgetManager.Model.AuthorizationModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BudgetManager.Authorization.TokenService
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly ITokenSettings _tokenSettings;

        public JwtTokenService(IOptions<TokenSettings> tokenSettings)
        {
            if (tokenSettings is null)
            {
                throw new ArgumentNullException(nameof(tokenSettings));
            }

            _tokenSettings = tokenSettings.Value;
        }

        public string CreateUserToken(ApplicationUser user, IList<string> userRoles)
        {
            List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("UserId", user.UserId.ToString()),
                        };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _tokenSettings.JwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
