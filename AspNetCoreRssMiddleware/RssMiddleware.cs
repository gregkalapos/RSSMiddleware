using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreRssMiddleware
{
    public class RssMiddleware
    {
        private readonly RequestDelegate _next;

        public RssMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }
}
