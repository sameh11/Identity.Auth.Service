﻿using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Identity.Auth.Web
{
    public static class DatabaseInitializer
    {
        public static void PopulateIdentityServer(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            //context.Database.Migrate();

            foreach (var client in Config.Clients)
            {
                var item = context.Clients.SingleOrDefault(c => c.ClientName == client.ClientId);

                if (item == null)
                {
                    var entity = client.ToEntity();
                    context.Clients.Add(entity);
                }
            }

            foreach (var resource in Config.ApiResources)
            {
                var item = context.ApiResources.SingleOrDefault(c => c.Name == resource.Name);

                if (item == null)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
            }

            foreach (var scope in Config.ApiScopes)
            {
                var item = context.ApiScopes.SingleOrDefault(c => c.Name == scope.Name);

                if (item == null)
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }
            }

            context.SaveChanges();
        }
    }
}
