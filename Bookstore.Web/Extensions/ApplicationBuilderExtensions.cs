using Bookstore.Web.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Bookstore.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
