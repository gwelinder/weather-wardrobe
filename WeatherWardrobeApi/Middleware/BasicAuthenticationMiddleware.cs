using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace WeatherWardrobeApi.Middleware
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value;

            // Allow unauthenticated access to certain endpoints
            if (path.StartsWith("/api/auth") || path.StartsWith("/api/weather") || path.StartsWith("/api/clothingitems/recommendations"))
            {
                await _next(context);
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials)).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                if (IsAuthorizedUser(username, password))
                {
                    await _next(context);
                    return;
                }
            }

            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }

        private bool IsAuthorizedUser(string username, string password)
        {
            // Replace with your own validation logic or check against a database
            return username == "admin" && password == "password";
        }
    }
}
