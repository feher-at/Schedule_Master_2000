using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Npgsql.Logging;
using Schedule_master_2000.Services;

namespace Schedule_master_2000
{
    public class Startup
    {
        public sealed class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
        {
            private static void RemoveReturnUrlFromRedirectUri(RedirectContext<CookieAuthenticationOptions> context)
            {
                var ub = new UriBuilder(context.RedirectUri);
                var query = QueryHelpers.ParseQuery(ub.Query);
                ub.Query = null;
                query.Remove("ReturnUrl");
                context.RedirectUri = ub.Uri.ToString();
                foreach (var key in query.Keys)
                {
                    context.RedirectUri = QueryHelpers.AddQueryString(context.RedirectUri, key, query[key]);
                }
            }

            public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
            {
                RemoveReturnUrlFromRedirectUri(context);
                return base.RedirectToAccessDenied(context);
            }

            public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
            {
                RemoveReturnUrlFromRedirectUri(context);
                return base.RedirectToLogin(context);
            }
        }

        private readonly string connectionString;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Debug, true, true);
                NpgsqlLogManager.IsParameterLoggingEnabled = true;
            }
            Configuration = configuration;
            connectionString = InitConnectionString();
        }
        private string InitConnectionString()
        {
            string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Host=localhost;Username=postgres;Password=admin;Database=schedulemaster";
            return connectionString;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.EventsType = typeof(CustomCookieAuthenticationEvents));
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "670423635675-t3i8gg6jdp8hnoaisqcadbt84aau40gi.apps.googleusercontent.com";
                    options.ClientSecret = "O0WEALU0XEmo1ZHqZHTTLSW3";
                });
            services.AddScoped<CustomCookieAuthenticationEvents>();
            services.AddScoped<IUserService, SqlUserService>();
            services.AddScoped<IDbConnection>(_ =>
            {
                var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return connection;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();          
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
