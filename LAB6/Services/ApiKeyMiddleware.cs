using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading.Tasks;

namespace LAB6.Services
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "API-KEY";
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing");
                return;
            }

            var apiKeys = _configuration.GetSection("ApiKeys").Get<string[]>();
            string apiKeyString = extractedApiKey.ToString();

            if (!apiKeys.Contains(apiKeyString))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }

            await _next(context);
        }
    }
}