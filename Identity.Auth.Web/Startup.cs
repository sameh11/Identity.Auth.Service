using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Identity.Auth.Web.Services;
using System.Reflection;
using Domain.Models;
using Identity.Auth.Web.Migrations.Data;
using IdentityServer4.Services;
using System.Security.Claims;
using IdentityModel;
using Domain.Models.Parent;

namespace Identity.Auth.Web
{
    public class Startup
    {
        public Startup(IConfiguration config) => Configuration = config;
        private IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationAssembly = typeof(ApplicationIdentityDbContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddCors(setup =>
            {
                setup.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.WithOrigins("http://localhost:7077", "http://localhost:5229", "https://localhost:7064 ");
                    policy.AllowCredentials();
                });
            });

            services.AddDbContext<ProductDbContext>(opts =>
            {
                opts.UseSqlServer(
                Configuration["ConnectionStrings:AppDataConnection"],
                opts=>opts.MigrationsAssembly(migrationAssembly));

            });
            services.AddDbContext<ApplicationIdentityDbContext>(opts =>
            {
                opts.UseSqlServer(
                Configuration["ConnectionStrings:IdentityConnection"],
                opts => opts.MigrationsAssembly(migrationAssembly)
                );
            });
            services.AddDbContext<ParentDbContext>(opts =>
            {
                opts.UseSqlServer(
                Configuration["ConnectionStrings:ParentDataConnection"],
                opts => opts.MigrationsAssembly(migrationAssembly)
                );
            });

            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.SignIn.RequireConfirmedPhoneNumber = false;
                opts.SignIn.RequireConfirmedEmail = false;
                opts.SignIn.RequireConfirmedAccount = false;
                opts.Stores.ProtectPersonalData = false;
                opts.User.RequireUniqueEmail = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.Password.RequiredLength = 0;
                opts.Lockout.MaxFailedAccessAttempts = 5;

            }).AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>{
                //options.UserInteraction.LoginUrl = "https://localhost:44350/Areas/Identity/SignIn";
                //options.UserInteraction.LogoutUrl = "https://localhost:44350/Areas/Identity/SignOut";
                options.EmitStaticAudienceClaim = true;
            })
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<IdentityUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"], 
                    sql => sql.MigrationsAssembly(migrationAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"], 
                    sql => sql.MigrationsAssembly(migrationAssembly));
            })
            .AddProfileService<ProfileService>();

            services.ConfigureApplicationCookie(options =>
             {
                 // Cookie settings
                 options.Cookie.HttpOnly = true;
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                 options.LoginPath = "/Identity/Account/Login";
                 options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                 options.SlidingExpiration = true;
             });
            services.AddAuthorization();
            services.AddScoped<TokenUrlEncoderService>();
            services.AddScoped<IdentityEmailService>();
            services.AddScoped<IEmailSender, ConsoleEmailSender>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.PopulateIdentityServer();
                app.SeedUserStoreForDashboard();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
            app.SeedUserStoreForDashboard();
        }
    }
}