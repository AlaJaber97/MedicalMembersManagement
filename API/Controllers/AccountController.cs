using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<BLL.Modals.Identity.User> _signInManager;
        private readonly UserManager<BLL.Modals.Identity.User> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<BLL.Modals.Identity.User> userManager, SignInManager<BLL.Modals.Identity.User> signInManager,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] BLL.Modals.Identity.Login LoginRequest)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(LoginRequest.Username, LoginRequest.Password, LoginRequest.RememberMe, false).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users.SingleOrDefaultAsync(r => r.UserName == LoginRequest.Username);
                    return Ok(API.Services.JWT.GenerateToken(appUser, _configuration));
                }
                else
                {
                    if (result.IsLockedOut) return BadRequest("Is locked out");
                    if (result.IsNotAllowed) return BadRequest("Is not allowed");
                    return BadRequest("User name or password not correct");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] BLL.Modals.Identity.Register RegisterRequest)
        {
            try
            {
                var user = new BLL.Modals.Identity.User(RegisterRequest.Username);
                var result = await _userManager.CreateAsync(user, RegisterRequest.Password);
                if (result.Succeeded)
                {
                    return Ok(API.Services.JWT.GenerateToken(user, _configuration));
                }
                return StatusCode(500, result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
