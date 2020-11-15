// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace MyIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("product.read"),
                new ApiScope("product.write"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "product", 
                    DisplayName = "Product Api", 
                    Scopes = new List<string> { "product.read", "product.write" }
                },

                new ApiResource
                {
                    Name = "productClientCredentials",
                    DisplayName = "Product Api",
                    Scopes = new List<string> { "product.read", "product.write" },
                    ApiSecrets = new List<Secret> {new Secret("scopesecret".Sha256())},
                },
            };

        public static IEnumerable<Client> Clients(Dictionary<string, string> clientsUrl) =>
            new Client[]
            {
                new Client
                {
                    ClientId = "productswaggerui",
                    ClientName = "Product Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{clientsUrl["ProductApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientsUrl["ProductApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "product.read"
                    }
                },

                new Client
                {
                    ClientId = "productclient",
                    ClientName = "Product Swagger UI",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //AllowAccessTokensViaBrowser = true,
                    
                    ClientSecrets = new List<Secret> {new Secret("secret".Sha256())},
                    
                    //AllowedCorsOrigins = {"https://localhost:5001"},
                    //AllowedCorsOrigins = {"https://localhost:44320"},
                    
                    //RedirectUris = { $"{clientsUrl["ProductApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientsUrl["ProductApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "product.read"
                    }
                },
            };
    }
}