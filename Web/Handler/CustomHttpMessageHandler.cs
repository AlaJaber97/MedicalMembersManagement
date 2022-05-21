namespace Web.Handler
{
    public class CustomHttpMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _context;
        public CustomHttpMessageHandler(IHttpContextAccessor context)
        {
            _context = context;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token_value = _context.HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token_value))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token_value);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
