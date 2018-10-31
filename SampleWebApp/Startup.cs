using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
