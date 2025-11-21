using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PersonalWeb.Services
{
    public class NoAntiforgery : IAntiforgery
    {
        public AntiforgeryTokenSet GetAndStoreTokens(HttpContext httpContext)
            => new AntiforgeryTokenSet("", "", "", "");

        public AntiforgeryTokenSet GetTokens(HttpContext httpContext)
            => new AntiforgeryTokenSet("", "", "", "");

        public Task<bool> IsRequestValidAsync(HttpContext httpContext)
            => Task.FromResult(true);

        public Task ValidateRequestAsync(HttpContext httpContext)
            => Task.CompletedTask;

        public void SetCookieTokenAndHeader(HttpContext httpContext)
        {
            // No hacemos nada: desactiva cookie antiforgery
        }
    }
}
