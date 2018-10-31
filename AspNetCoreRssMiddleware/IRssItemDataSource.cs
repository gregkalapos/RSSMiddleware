using System;
using System.Collections.Generic;

namespace AspNetCoreRssMiddleware
{
    /// <summary>
    /// Rss item data source. This must be implemented by the user of the RSSMiddleware.
    /// The RssMidleware iterates through the result of the RssItems property and generates an item tag in the RSS fead for each RssFeedItem
    /// </summary>
    public interface IRssItemDataSource
    {
        /// <summary>
        /// Return all items that you want to see in your RSS feed
        /// </summary>
        /// <value>The rss items.</value>
        IEnumerable<RssFeedItem> RssItems { get; }
    }
}
