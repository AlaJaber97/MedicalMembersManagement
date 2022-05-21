using API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<BLL.Modals.Identity.User> _userManager;

        public SeedsController(UserManager<BLL.Modals.Identity.User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        // GET: api/Seeds/Fill
        [HttpGet("Fill")]
        public async Task<ActionResult> GetSeeds()
        {
            var user = new BLL.Modals.Identity.User("admin");
            var result = await _userManager.CreateAsync(user, "admin");
            if (result.Succeeded)
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
    }
}
