using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace Web.Controllers
{
    [Web.Attributes.Authorize]
    [Route("[controller]/[action]")]
    public class MemberController : Controller
    {
        private HttpClient _httpClient { get; set; }
        public MemberController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> List([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _httpClient.GetAsync($"api/Members");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<List<BLL.Modals.Member>>();
            return Json(result.ToDataSourceResult(request));
        }
        [HttpPost]
        public async Task<IActionResult> Create([DataSourceRequest] DataSourceRequest request, BLL.Modals.Member member)
        {
            if (member != null && ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Members/Create", member);
                response.EnsureSuccessStatusCode();
            }

            return Json(new[] { member }.ToDataSourceResult(request, ModelState));

        }
        [HttpPost]
        public async Task<IActionResult> Update([DataSourceRequest] DataSourceRequest request, BLL.Modals.Member member)
        {
            if (member != null && ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Members/Modify", member);
                response.EnsureSuccessStatusCode();
            }

            return Json(new[] { member }.ToDataSourceResult(request, ModelState));
        }
        [HttpPost]
        public async Task<IActionResult> Destroy([DataSourceRequest] DataSourceRequest request, BLL.Modals.Member member)
        {
            if (member != null)
            {
                var response = await _httpClient.DeleteAsync($"api/Members/Delete/{member.ID}");
                response.EnsureSuccessStatusCode();
            }

            return Json(new[] { member }.ToDataSourceResult(request, ModelState));
        }
    }
}
