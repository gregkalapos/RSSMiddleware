using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreRssMiddleware
{
    public class RssMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RssFeedDescriptor _rssFeedDescriptor;
        private readonly IRssItemDataSource _rssItemDataSource;

        public RssMiddleware(RequestDelegate next, RssFeedDescriptor rssFeedDescriptor, IRssItemDataSource rssItemDataSource)
        {
            _next = next;
            _rssFeedDescriptor = rssFeedDescriptor;
            _rssItemDataSource = rssItemDataSource;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.ToLower() == $"/{_rssFeedDescriptor.Path}")
            {
                context.Response.ContentType = "application/xml";

                await context.Response.WriteAsync(CreateXml(context).ToString());
            }
            else
            {
                await _next(context);
            }
        }

        public XElement CreateXml(HttpContext context)
        {
            var allItems = _rssItemDataSource.RssItems;

            XNamespace ns = "http://www.w3.org/2005/Atom";
            var rss = new XElement("rss", new XAttribute("version", "2.0"), new XAttribute(XNamespace.Xmlns + "atom", ns));

            var channel = new XElement("channel",
                                       new XElement("title", _rssFeedDescriptor.Title),
                                       new XElement("link", $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}"),
                                       new XElement("description", _rssFeedDescriptor.Description),
                                       new XElement("language", _rssFeedDescriptor.Language),
                                       new XElement("copyright", $"Copyright {DateTime.UtcNow.Year} Gergely Kalapos"),
                                       new XElement("lastBuildDate", allItems.OrderByDescending(n => n.PublishDate).First().PublishDate.ToUniversalTime().ToString("r")),
                                       new XElement(ns + "link", new XAttribute("href", $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/{_rssFeedDescriptor.Path}"), new XAttribute("rel", "self"), new XAttribute("type", "application/rss+xml"))
             );

            if(!String.IsNullOrEmpty(_rssFeedDescriptor.Category))
            {
                channel.Add(new XElement("category", _rssFeedDescriptor.Category));
            }

            foreach (var post in allItems)
            {
                var postInRss = new XElement("item");
                postInRss.Add(new XElement("title", post.Title));
                postInRss.Add(new XElement("description", post.Description));
                postInRss.Add(new XElement("link", $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/" + post.RealativeLink));
                postInRss.Add(new XElement("author", post.Author));
                postInRss.Add(new XElement("pubDate", post.PublishDate.ToUniversalTime().ToString("r")));
                postInRss.Add(new XElement("guid", $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/{post.RealativeLink}#When{post.PublishDate.ToUniversalTime().ToString(CultureInfo.InvariantCulture).Replace(" ", String.Empty)}", new XAttribute("isPermaLink", "false")));
                channel.Add(postInRss);
            }

            rss.Add(channel);

            return rss;
        }
    }
}
