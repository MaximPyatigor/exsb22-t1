using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using API.Models;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper) 
        {
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

        /*
         [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password, string returnurl)
        {
                ApplicationUser appUser = await userManager.FindByEmailAsync(email);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnurl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or Password");
            
         
            return View();
        }
         */
    }
}
