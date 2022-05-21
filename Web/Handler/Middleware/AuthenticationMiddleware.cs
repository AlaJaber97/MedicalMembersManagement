using System.Security.Claims;
using System.Text;

namespace Web.Handler.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        // Dependency Injection
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                AttachAccountToContext(configuration,context, token);
            }
            await _next(context);
        }
        private void AttachAccountToContext(IConfiguration configuration, HttpContext context, string token)
        {
            try
            {
                var UserId = BLL.Services.JWT.ValidateJwtToken(configuration, token);
                // on successful jwt validation attach UserId to context
                context.Items["UserId"] = UserId;
            }
            catch
            {
                // if jwt validation fails then do nothing 
            }
        }
    }
}
