using System;
namespace AspNetCoreRssMiddleware
{
    /// <summary>
    /// Encapsulates all the details about the RSS feed. The values within this type are written into the channel tag in the RSS feed.
    /// </summary>
    public class RssFeedDescriptor
    {
        public readonly string Title;
        public readonly string Description;
        public readonly string Language;
        public readonly string Category;
        public readonly string Path;

        /// <summary>
        /// RssFeedDescriptor constructor. Initializes all fields within the RssFeedDescriptor.
        /// </summary>
        /// <param name="title">The title of the RSS feed. The value is written into the title tag.</param>
        /// <param name="description">The description of the RSS feed. The value is written into the description tag.</param>
        /// <param name="path">The relative path to the RSS feed within the site. For example if your site lives on 'www.mysite.com' and you set this value to myrss then the full path of your RSS will be 'www.mysite.com/myrss'. The detault value is 'rss'</param>
        /// <param name="language">Indicates the language of the feed. The value is written into the language tag</param>
        /// <param name="category">The category of the feed. The value is written into the category tag</param>
        public RssFeedDescriptor(string title, string description, string path = "rss", string language = "en-us", string category = "")
        {
            Title = title;
            Description = description;
            Language = language;
            Category = category;
            Path = path;
        }
    }
}
