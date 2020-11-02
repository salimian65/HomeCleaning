// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace HomeCleaning.IdentityProvider
{
    public class Config
    {
        private IConfiguration Configuration { get; }


        public Config(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
              //  new IdentityResource("roles", new[] { "role" })
            };
        }

        public IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
              //  new ApiResource("api1", "My API"),
              //  new ApiResource("roles", "My Roles"),
              //  new IdentityResource("roles", new[] { "role" })
                new ApiResource("backend", "Home Cleaning REST API"){ UserClaims = { JwtClaimTypes.Name } }
            };
        }


        public IEnumerable<Client> GetClients()
        {
            var webClient = Configuration["partner:webClient"];
            return new[]
            {
                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "frontend",
                    ClientName = "Home Cleaning JavaScript Client",
                    ClientUri = webClient,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris =
                    {
                      webClient,
                      webClient + "/callback",
                      webClient + "/silent",
                      webClient + "/popup",
                    },

                    PostLogoutRedirectUris = { webClient },
                    AllowedCorsOrigins = {     webClient },
                    AllowedScopes = { "openid", "profile", "backend" }
                    //AllowedScopes = { "openid",
                    //                  "profile",
                    //                  "roles",
                    //                   "api1",
                    //                   ClaimTypes.Role
                    //}
                  //  AllowedScopes = { "openid", "profile", "backend" }
                },

                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("sssss".Sha256())
                    },

                    AllowedScopes = { "backend" }
                },
            };
        }

        //public  IEnumerable<ApiScope> ApiScopes =>
        //    new ApiScope[]
        //    {
        //        new ApiScope("backend"),
        //        new ApiScope("scope2"),
        //    };
    }
}