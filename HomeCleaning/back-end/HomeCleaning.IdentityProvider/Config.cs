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
   public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new ApiResource[]
            {

                new ApiResource("backend", "MarketPlace REST API",
                    new [] {  JwtClaimTypes.Role,
                        JwtClaimTypes.Name,
                        "backend"}),
            };
        }

        //public static IEnumerable<ApiScope> ApiScopes =>
        //        new ApiScope[]
        //        {
        //            new ApiScope("backend", "My API"),
        //        };

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "frontend",
                    ClientName = "MarketPlace JavaScript Client",
                    ClientUri = "http://localhost:8080",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 60 , // 1.5 minutes
                    SlidingRefreshTokenLifetime=1296000,
                    AbsoluteRefreshTokenLifetime = 2592000 ,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RequireConsent = false,
                    ClientClaimsPrefix = string.Empty,

                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,

                    RedirectUris =
                    {
                        "http://localhost:8080/callback",
                        "http://localhost:8080/static/silent-renew.html"
                    },

                    PostLogoutRedirectUris = { "http://localhost:8080" },
                    AllowedCorsOrigins = { "http://localhost:8080" },

                    AllowedScopes = {
                                       IdentityServerConstants.StandardScopes.OpenId,
                                       IdentityServerConstants.StandardScopes.Profile,
                                       IdentityServerConstants.StandardScopes.OfflineAccess,
                                       "backend" }
                }
            };
        }
    }
}