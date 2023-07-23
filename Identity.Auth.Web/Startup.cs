using Identity.Auth.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Identity.Auth.Web.Areas.Identity.Data;

namespace Identity.Auth.Web
{
    public class Startup
    {
        public Startup(IConfiguration config) => Configuration = config;
        private IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<ProductDbContext>(opts =>
            {
                opts.UseSqlServer(
                Configuration["ConnectionStrings:AppDataConnection"]);
            });
            services.AddDbContext<IdentityDbContext>(opts =>
            {
                opts.UseSqlServer(
                Configuration["ConnectionStrings:IdentityConnection"],
                opts => opts.MigrationsAssembly("Identity.Auth.Web")
                );
            });
            services.AddDefaultIdentity<IdentityUser>(opts =>
            {
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                opts.Lockout.MaxFailedAccessAttempts = 5;

            })
            .AddEntityFrameworkStores<IdentityDbContext>();
            services.AddAuthentication();
            //services.AddFacebook(opts =>
            //{
            //    opts.AppId = Configuration["Facebook:AppId"];
            //    opts.AppSecret = Configuration["Facebook:AppSecret"];
            //})
            //.AddGoogle(opts =>
            //{
            //    opts.ClientId = Configuration["Google:ClientId"];
            //    opts.ClientSecret = Configuration["Google:ClientSecret"];
            //}); ;

            services.AddScoped<IEmailSender, ConsoleEmailSender>();
            services.AddHttpsRedirection(opts => opts.HttpsPort = 44350);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}