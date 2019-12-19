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

            services.AddAuthentication()
                    .AddCookie(ops => ops.LoginPath = "/api/values/login")
                    .AddGoogle(ops =>
                    {
                        ops.ClientId = "1061433997967-955692qk7fe7lq58tn6vi0hl8ba7t1on.apps.googleusercontent.com";
                        ops.ClientSecret = "xq65BxymyUab6-vSVLIvR4ii";
                        ops.CallbackPath = "/googlesignin"; 

                        ops.Events.OnCreatingTicket = context =>
                        {
                            context.HttpContext.User = context.Principal;
                            context.HttpContext.Response.Redirect("https://localhost:5051/googlesignin");
                            #region Leftovers
                            value = context.AccessToken;

                            //This also works in case above redirection won't work
                            //context.Backchannel.GetAsync("https://localhost:5051/googlesignin");

                            #endregion
                            return Task.CompletedTask;
                        };
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
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
