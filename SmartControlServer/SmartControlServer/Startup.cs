using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SmartControlServer.Models;

using System;

namespace SmartControlServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"\n{context.Request.Method} : {context.Request.Path}");

                string token = context.Request.Headers["Token"];
                if (token is null || AppSettings.Token != token)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Access denied !");
                    Console.Write($" => {context.Response.StatusCode}");
                    Console.ResetColor();
                    return;
                }

                await next.Invoke();
                Console.Write($" => {context.Response.StatusCode}");
                Console.ResetColor();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}