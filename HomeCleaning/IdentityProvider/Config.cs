// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace IdentityProvider
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
            };
        }

        public IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
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
                    ClientUri =Configuration["partner:client"],

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

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
                }
            };
        }
    }
}