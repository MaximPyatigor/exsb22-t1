using BudgetManager.Authorization;
using BudgetManager.SDK.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : Controller
    {
        private readonly IAuthorizationManager _authorizationManager;
        public AuthController(IAuthorizationManager authorizationManager)
        {
            _authorizationManager = authorizationManager ?? throw new ArgumentNullException(nameof(authorizationManager));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginData)
        {
            var result = await _authorizationManager.LoginAsync(loginData.Email, loginData.Password);
            return Ok(result);
        }
    }
}
