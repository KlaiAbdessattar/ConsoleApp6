using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp6.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication1
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
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            DependencyInjectionHelper.LoadDBContext(services);
            DependencyInjectionHelper.LoadDAOs(services);
            DependencyInjectionHelper.LoadServices(services);
            services.AddCors(options =>
            {
                options.AddPolicy("DEV_POLICY",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials();
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx => {
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:4200");
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "*");
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Methods", "*");
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
                },
                //FileProvider = new PhysicalFileProvider(
                //  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "exports")),
                //RequestPath = "/api/exports"
            });
            app.UseCors("DEV_POLICY");
            app.UseHttpsRedirection();

            app.UseRouting();

           

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
