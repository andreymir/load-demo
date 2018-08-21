using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheManager.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCacheManagerConfiguration(builder =>
            {
                builder
                    .WithRedisConfiguration("redis", "localhost:6379")
                    .WithUpdateMode(CacheUpdateMode.Up)
                    .WithSerializer(typeof(CacheManager.Serialization.Json.GzJsonCacheSerializer))
                    .WithMaxRetries(1)
                    .WithRetryTimeout(2)
                    .WithRedisCacheHandle("redis");
            });
            services.AddCacheManager();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async context =>
            {
                var cache = context.RequestServices.GetService<ICacheManager<object>>();
                var value = cache.Get<string>($"myvalue_{Guid.NewGuid()}")
                            ?? "no value";
                await context.Response.WriteAsync(value);
            });
        }
    }
}