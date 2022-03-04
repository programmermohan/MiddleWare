using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UpdateResponseHeader
    {
        private readonly RequestDelegate _next;

        public UpdateResponseHeader(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;

                //All pages should be served over HTTPS
                httpContext.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
                //This header prevents MIME sniffing, which can be used by attackers
                httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                /* X-Xss-Protection header will cause most modern browsers to stop loading the page when a cross-site scripting attack is identified.*/
                httpContext.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                //to prevent framing means it prevents browsers from rendering your web page within another web page
                httpContext.Response.Headers.Add("X-Frame-Options", "DENY");
                //Content-Security-Policy header helps to prevent code injection attacks like cross-site scripting and clickjacking
                httpContext.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");

                /*Referrer-Policy header will prevent the refer in link When we click a link on a website, 
                 * the calling URL is automatically transferred to the linked site*/
                httpContext.Response.Headers.Add("Referrer-Policy", "no-referrer");
                //X-Permitted-Cross-Domain-Policies header protects you against website spoofing or unauthorized use of your content.
                httpContext.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");

                //Permissions-Policy header (formerly known as Feature-Policy) tells the browser which platform features your website needs
                //httpContext.Response.Headers.Add("Feature-Policy", "camera 'none'; geolocation 'none'; microphone 'none'; usb 'none'");
                httpContext.Response.Headers.Add("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");

                return Task.CompletedTask;

            }, httpContext);
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UpdateResponseHeaderExtensions
    {
        public static IApplicationBuilder UseUpdateResponseHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UpdateResponseHeader>();
        }
    }
}
