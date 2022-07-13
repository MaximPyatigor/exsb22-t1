using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.JwtTokenService;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _tokenService;

        public AuthController(UserManager<ApplicationUser> userManager, IMapper mapper, IJwtTokenService tokenService) {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserDto user)
        {
            var userApp = _mapper.Map<ApplicationUser>(user);

            IdentityResult result = await _userManager.CreateAsync(userApp, user.Password);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var token = _tokenService.CreateUserToken(user, userRoles);

                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
