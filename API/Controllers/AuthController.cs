using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using API.Models;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public AuthController(UserManager<ApplicationUser> userManager, IMapper mapper) {
            _userManager = userManager;
            _mapper = mapper;
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Register(UserDto user)
        //{
        //    var userApp = _mapper.Map<ApplicationUser>(user); 

        //    IdentityResult result = await _userManager.CreateAsync(userApp, user.Password);
        //    return Ok();
        //}
    }
}
