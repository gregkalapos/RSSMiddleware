using System;
using Microsoft.AspNetCore.Builder;

namespace AspNetCoreRssMiddleware
{
    public static class RssMiddlewareExtensions
    {
        public static IApplicationBuilder UseRss(
            this IApplicationBuilder builder, RssFeedDescriptor rssFeedDescriptor, IRssItemDataSource rssItemDataSource)
        {
            return builder.UseMiddleware<RssMiddleware>(rssFeedDescriptor, rssItemDataSource);
        }
    }
}
