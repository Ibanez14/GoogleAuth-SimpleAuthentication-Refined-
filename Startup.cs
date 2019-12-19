using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Google_webAPI
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string value = default(string);

            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddCookie("Cookies")
                    .AddGoogle(ops =>
                    {
                        ops.ClientId = "1061433997967-955692qk7fe7lq58tn6vi0hl8ba7t1on.apps.googleusercontent.com";
                        ops.ClientSecret = "xq65BxymyUab6-vSVLIvR4ii";
                        ops.CallbackPath = "/auth/registerexternal";
                        ops.Events.OnTicketReceived = context =>
                        {
                            context.SkipHandler();
                            return Task.CompletedTask;
                        };
                    });






            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();

            //app.UseCors(ops =>
            //{
            //    ops.AllowAnyOrigin().AllowAnyMethod();
            //});


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
