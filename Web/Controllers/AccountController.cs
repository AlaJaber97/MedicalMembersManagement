using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private const string ApiUri = "https://localhost:7270/api";
        private readonly HttpClient _httpClient;
        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [Route("~/")]
        [Route("/Account")]
        [Route("~/Account/Login")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(BLL.Modals.Identity.Login LoginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUri}/Account/login", LoginRequest);
            if (response.IsSuccessStatusCode)
            {
                var access_token = await response.Content.ReadAsStringAsync();

                HttpContext.Session.SetString("Token", access_token);
                if (LoginRequest.RememberMe)
                    HttpContext.Response.Cookies.Append("Token", access_token,new CookieOptions
                    {
                        Secure = true,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                    });

                return RedirectToAction("Index", "Member");
            }
            ModelState.AddModelError("",await response.Content.ReadAsStringAsync());
            LoginRequest.Password = string.Empty;
            return View(LoginRequest);
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear(); 
            HttpContext.Response.Cookies.Delete("Token");

            return RedirectToAction(nameof(Login));
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(BLL.Modals.Identity.Register RegisterRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ApiUri}/Account/Register", RegisterRequest);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Member");
            }
            RegisterRequest.Password = string.Empty;
            RegisterRequest.ConfirmPassword = string.Empty;
            return View(RegisterRequest);
        }
    }
}
