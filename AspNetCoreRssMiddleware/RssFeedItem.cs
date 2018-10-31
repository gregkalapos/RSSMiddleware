using System;
namespace AspNetCoreRssMiddleware
{
    public class RssFeedItem
    {
        public readonly string Title;
        public readonly string Description;
        public readonly string Author;
        public readonly String RealativeLink;
        public readonly DateTime PublishDate;

        public string RssFeedGuid => $"{RealativeLink}#When{PublishDate.ToUniversalTime()}";


        public RssFeedItem(string title, string description, string author, string realativeLink, DateTime pubDate)
        {
            Title = title;
            Description = description;
            Author = author;
            RealativeLink = realativeLink;
            PublishDate = pubDate;
        }
    }
}
