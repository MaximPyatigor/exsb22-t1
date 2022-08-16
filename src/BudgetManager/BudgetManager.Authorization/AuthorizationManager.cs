using BudgetManager.Authorization.TokenService;
using BudgetManager.CQRS.Commands.RefreshTokenCommands;
using BudgetManager.CQRS.Queries.RefreshTokenQueries;
using BudgetManager.CQRS.Queries.UserQueries;
using BudgetManager.Model.AuthorizationModels;
using BudgetManager.Shared.Utils.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BudgetManager.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _tokenService;

        public AuthorizationManager(UserManager<ApplicationUser> userManager,
            IMediator mediator,
            IJwtTokenService tokenService)
        {
            _userManager = userManager;
            _mediator = mediator;
            _tokenService = tokenService;
        }

        public async Task<bool> Register(string email, string password, Guid userId,
            bool isAdmin)
        {
            var appUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
                UserId = userId
            };

            var savedAppUser = await _userManager.CreateAsync(appUser, password);

            if (isAdmin)
            {
                await _userManager.AddToRolesAsync(appUser, new string[] { "Admin", "User" });
            }
            else
            {
                await _userManager.AddToRoleAsync(appUser, "User");
            }
            
            return savedAppUser.Succeeded;
        }

        public async Task<AuthorizationResponse> Login(string email, string password)
        {
            try
            {
                var appUser = await _userManager.FindByEmailAsync(email);

                var user = await _mediator.Send(new GetUserByIdQuery(appUser.UserId));

                if (appUser != null & await _userManager.CheckPasswordAsync(appUser, password)
                    & user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(appUser);

                    var token = _tokenService.CreateUserToken(appUser, userRoles);
                    var refreshToken = await _tokenService.CreateRefreshToken(user.Id);
                    var refreshTokenResult = await _mediator.Send(new AddRefreshTokenCommand(refreshToken));
                    
                    var responseUser = new UserAuthorizationObject
                    {
                        Id = user.Id,
                        DOB = user.DOB,
                        Country = user.Country,
                        DefaultCurrency = user.DefaultCurrency,
                        DefaultWallet = user.DefaultWallet,
                        Email = user.Email,
                        FullName = user.FullName,
                        IsAdmin = await _userManager.IsInRoleAsync(appUser, "Admin")
                    };
                    var response = new AuthorizationResponse
                    {
                        User = responseUser,
                        Token = token,
                        RefreshToken = refreshToken.Token
                    };
                    return response;
                } else
                {
                    throw new UnauthorizedAccessException("User not found or password is not correct");
                }
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException("User not found or password is not correct");
            }

        }

        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersList()
        {
            return await Task.Run(() => _userManager.Users.ToList());
        }

        public async Task<RefreshedTokenResponse> RefreshToken(RefreshedTokenResponse tokenResponse)
        {
            var accessToken = tokenResponse.Token;
            var refreshToken = tokenResponse.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            if (principal == null) { throw new AppException("Invalid access token or refresh token"); }

            Guid userId = Guid.Parse(principal.FindFirst("UserId").Value);
            var email = principal.FindFirst(ClaimTypes.Email).Value;

            var appUser = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(appUser);
            var user = await _mediator.Send(new GetUserByIdQuery(userId));
            var token = await _mediator.Send(new GetRefreshTokenByTokenQuery(refreshToken));

            if (appUser.UserId != user.Id ||
                user == null || user.Id != token.UserId ||
                token.Token != refreshToken || token.Expires <= DateTime.Now.ToUniversalTime())
            {
                throw new AppException("Invalid access token or refresh token");
            }

            var newAccessToken = _tokenService.CreateUserToken(appUser, roles);
            var newRefreshToken = await _tokenService.CreateRefreshToken(user.Id);

            await _mediator.Send(new UpdateRefreshTokenCommand(newRefreshToken));

            return new RefreshedTokenResponse
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };
        }
    }
}
