

using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SimemNetAdmin.Web.Api
{
    [ExcludeFromCodeCoverage]
    public class AuthMiddleWare
    {
        private readonly RequestDelegate next;

        public AuthMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? url = context.Request.Path.Value;
            string? app = context.Request.Headers["site"].ToString();
            if (app=="Terceros" && !string.IsNullOrEmpty(url) && !url.ToString().Contains("ValidateUser"))
            {
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var header = context.Request.Headers["Authorization"].ToString();
                var token = header.ToString().Substring(7);
                var tokenValidation = Transversal.Helper.JwtHandler.ValidateToken(token);

                if (string.IsNullOrEmpty(tokenValidation))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                context.Items["company"] = context.Request.Headers["company"].ToString();
            }
           
            await next(context);

        }

    }
}