using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Musical_WebStore_BlazorApp.Server.Data;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;

namespace Musical_WebStore_BlazorApp.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
        {
            string connstr = GetConnectionString(env);

            services.AddDbContext<MusicalShopIdentityDbContext>(
                options => options.UseSqlServer(connstr));

            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        private string GetConnectionString(IWebHostEnvironment env)
        {
            string dbName;

            if (env.IsDevelopment())
            {
                dbName = "AuthenticationDB_Local";
            }
            else
            {
                dbName = "AuthenticationDB";
            }

            return Configuration.GetConnectionString(dbName);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Client.Startup>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
            });
        }
    }
}