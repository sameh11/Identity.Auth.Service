using Identity.Auth.Web.Controllers;
using Identity.Auth.Web.Migrations.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Auth.Web
{
    public static class DashBoardSeed
    {
        public static List<string> rolesData = new List<string> { "Admin", "Dashboard", "Parent", "ServiceProvider" };
        public static List<IdentityUser> usersData = new List<IdentityUser>
                {
                    new IdentityUser
                    {
                        UserName =  "admin@example.com",
                        Email =  "admin@example.com",
                        EmailConfirmed = true,
                    },
                    new IdentityUser
                    {
                        UserName =  "dashboard@example.com",
                        Email =  "dashboard@example.com",
                        EmailConfirmed = true,
                    },
                    new IdentityUser
                    {
                        UserName =  "parent@example.com",
                        Email =  "parent@example.com",
                        EmailConfirmed = true,
                    },
                };

        public static void SeedUserStoreForDashboard(this IApplicationBuilder app)
        {

            SeedStore(app).GetAwaiter().GetResult();
        }

        private async static Task SeedStore(IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                IConfiguration config =
                    scope.ServiceProvider.GetService<IConfiguration>();
                UserManager<IdentityUser> userManager =
                    scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager =
                    scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                string roleName = config["Dashboard:Role"] ?? "Admin";
                //string userName = config["Dashboard:User"] ?? "Admin1";
                string email = config["Dashboard:Email"] ?? "admin@example.com";
                string password = config["Dashboard:Password"] ?? "mysecret";


                foreach (var role in rolesData)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                foreach (var user in usersData)
                {

                    IdentityUser defaultUser =
                        await userManager.FindByEmailAsync(user.Email);
                    if (defaultUser == null)
                    {
                        var u = new IdentityUser { Email = user.Email, UserName = user.UserName, EmailConfirmed = user.EmailConfirmed };
                        await userManager.CreateAsync(u);
                        var identityResult = userManager.FindByEmailAsync(u.Email).Result;
                        await userManager.AddPasswordAsync(u, password);
                        if (!await userManager.IsInRoleAsync(u, roleName))
                        {
                            await userManager.AddToRoleAsync(u, roleName);
                        }
                    }
                }
            }
        }
    }
}
