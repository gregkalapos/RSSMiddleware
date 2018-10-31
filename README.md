# RSSMiddleware
RSS Middleware for ASP.NET Core


NuGet package available on [NuGet.org](https://www.nuget.org/packages/AspNetCoreRssMiddleware/)

In order to use this middleware you have to register it in the Configure method of your ASP.NET Core application (typically in the Startup.cs file).


```
using AspNetCoreRssMiddleware;

namespace SampleWebApp
{
    public class Startup
    {   
    
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRss(RssFeedDescriptor, IRssItemDataSource);
            //...regisrtering other middlewares
        }
    }
}
```

The UseRSS extension method expects 2 parameters:
* An RssFeedDescriptor instance: It encapsulates all the details about the RSS feed. It has 2 mandatory properties: The title and the description of the feed.
* An implementation of the IRssItemDataSource interface 


## IRssItemDataSource

This interface has a single method that returns all the items that will be placed into the RSS feed.

```IEnumerable<RssFeedItem> RssItems { get; }```


## Complete sample

```
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using AspNetCoreRssMiddleware;

namespace SampleWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRss(new RssFeedDescriptor("sampleproj", "sample RSS feed", category: "Software Engineering"), new DummyRssItemDataSource());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }

    public class DummyRssItemDataSource : IRssItemDataSource
    {
        public IEnumerable<RssFeedItem> RssItems =>
        new List<RssFeedItem>
        {
            new RssFeedItem("Post1", "Description of post 1", "gergo@kalapos.net (Gergely Kalapos)", "ShowPost/Post1", new DateTime(2017, 2, 4, 1, 23, 1)),
            new RssFeedItem("Post2", "Description of post 2", "gergo@kalapos.net (Gergely Kalapos)", "ShowPost/Post2", new DateTime(2018, 5, 13, 9, 11, 43)),
            new RssFeedItem("Post3", "Description of post 3", "gergo@kalapos.net (Gergely Kalapos)", "ShowPost/Post3", new DateTime(2018, 9, 13, 17, 46, 5))
        };
    }
}

```
