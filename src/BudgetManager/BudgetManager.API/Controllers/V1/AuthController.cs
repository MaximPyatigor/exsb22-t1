using BudgetManager.Authorization;
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
            _authorizationManager = authorizationManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _authorizationManager.Login(email, password);
            return Ok(result);
        }
    }
}
