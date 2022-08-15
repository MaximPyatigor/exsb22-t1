using BudgetManager.Model.AuthorizationModels;
using Microsoft.Extensions.Options;
using BudgetManager.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using BudgetManager.Shared.DataAccess.MongoDB.BaseImplementation;

namespace BudgetManager.Authorization.TokenService
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly ITokenSettings _tokenSettings;

        public JwtTokenService(IOptions<TokenSettings> tokenSettings)
        {
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
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<RefreshToken> CreateRefreshToken(Guid userId)
        {
            var refreshToken = new RefreshToken()
            {
                UserId = userId,
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(3),
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))
            };

            return refreshToken;
        }
    }
}
