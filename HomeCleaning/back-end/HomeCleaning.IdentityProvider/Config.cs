// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
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
            };
        }

        public IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("backend", "MarketPlace REST API", new [] { JwtClaimTypes.Role,JwtClaimTypes.Name}),
            };
        }

        public IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("backend",new [] { JwtClaimTypes.Role,JwtClaimTypes.Name}),
            };

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

                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 90, // 1.5 minutes
                    AbsoluteRefreshTokenLifetime = 0,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientClaimsPrefix = string.Empty,

                    RedirectUris =
                    {
                      webClient + "/callback",
                      webClient + "/static/silent-renew.html",
                     },

                    PostLogoutRedirectUris = { webClient },
                    AllowedCorsOrigins = { webClient },
                    AllowedScopes = {  IdentityServerConstants.StandardScopes.OpenId,
                                       IdentityServerConstants.StandardScopes.Profile, 
                                       "backend" }
                   
                }
            };
        }

    }
}