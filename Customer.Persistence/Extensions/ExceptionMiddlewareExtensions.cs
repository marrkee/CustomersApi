namespace Customers.Persistence.Extensions
{
    using Customers.Persistence.CustomExceptionMiddleware;
    using Microsoft.AspNetCore.Builder;

    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
