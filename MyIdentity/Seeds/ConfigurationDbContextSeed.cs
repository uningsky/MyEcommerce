using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIdentity.Seeds
{
    public class ConfigurationDbContextSeed
    {
        public async Task SeedAsync(ConfigurationDbContext context, IConfiguration configuration)
        {
            var clientUrls = new Dictionary<string, string>();

            clientUrls.Add("ProductApi", configuration.GetValue<string>("ProductApiClient"));


            var clients = context.Clients.ToList();
            context.Clients.RemoveRange(clients);

            var identityResources = context.IdentityResources.ToList();
            context.IdentityResources.RemoveRange(identityResources);

            var apiScopes = context.ApiScopes.ToList();
            context.ApiScopes.RemoveRange(apiScopes);

            var apiResources = context.ApiResources.ToList();
            context.ApiResources.RemoveRange(apiResources);

            context.SaveChanges();


            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients(clientUrls))
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
