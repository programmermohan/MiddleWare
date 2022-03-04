using CustomMiddleWare.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValidateClientKey
    {
        private readonly RequestDelegate _next;

        public ValidateClientKey(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IClientRepository clientRepository)
        {
            try
            {
                if (!httpContext.Request.Path.Value.Contains("swagger")) // remove for the swagger localhost
                {
                    Microsoft.Extensions.Primitives.StringValues QueryVal;
                    bool IsClientExist = httpContext.Request.Query.TryGetValue("Client-Key", out QueryVal); //if client is sending client key in the query string

                    if (!httpContext.Request.Headers.Keys.Contains("Client-Key") && !IsClientExist)
                    {
                        httpContext.Response.StatusCode = 400; //Bad Request                
                        await httpContext.Response.WriteAsync("Client Key is missing");
                        return;
                    }
                    else
                    {
                        QueryVal = string.IsNullOrEmpty(QueryVal) ? httpContext.Request.Headers["Client-Key"] : QueryVal;

                        if (!clientRepository.CheckValidClientKey(QueryVal))
                        {
                            httpContext.Response.StatusCode = 401; //UnAuthorized
                            await httpContext.Response.WriteAsync("Invalid Client Key");
                            return;
                        }
                    }
                }
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message + "\n" + ex.InnerException);
#endif
                throw;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ValidateClientKeyExtensions
    {
        public static IApplicationBuilder UseValidateClientKey(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ValidateClientKey>();
            return builder;
        }
    }
}
