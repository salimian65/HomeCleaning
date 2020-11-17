// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace HomeCleaning.IdentityProvider2
{
    //public class Config
    //{
    //    private IConfiguration Configuration { get; }


    //    public Config(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }


    //    public IEnumerable<IdentityResource> GetIdentityResources()
    //    {
    //        return new IdentityResource[]
    //        {
    //            new IdentityResources.OpenId(),
    //            new IdentityResources.Profile(),
    //        };
    //    }

    //    public IEnumerable<ApiResource> GetApiResources()
    //    {
    //        return new ApiResource[]
    //        {//, new [] { JwtClaimTypes.Role,JwtClaimTypes.Name}
    //            new ApiResource("backend", "MarketPlace REST API"),
    //        };
    //    }

    //    public IEnumerable<ApiScope> ApiScopes =>
    //        new ApiScope[]
    //        {
    //            new ApiScope("backend", "My API"),
    //        };

    //    public IEnumerable<Client> GetClients()
    //    {
    //        var webClient = Configuration["partner:webClient"];
    //        return new[]
    //        {
    //            // SPA client using code flow + pkce
    //            new Client
    //            {
    //                ClientId = "frontend",
    //                ClientName = "Home Cleaning JavaScript Client",
    //                ClientUri = webClient,

    //                AllowedGrantTypes = GrantTypes.Code,
    //                RequirePkce = true,
    //                RequireClientSecret = false,
    //                RequireConsent = false,

    //                AllowOfflineAccess = true,
    //                AccessTokenLifetime = 3600, // 1.5 minutes
    //                AbsoluteRefreshTokenLifetime = 0,
    //                RefreshTokenUsage = TokenUsage.OneTimeOnly,
    //                RefreshTokenExpiration = TokenExpiration.Sliding,
    //                UpdateAccessTokenClaimsOnRefresh = true,
    //                ClientClaimsPrefix = string.Empty,

    //                RedirectUris =
    //                {
    //                  webClient + "/callback",
    //                  webClient + "/static/silent-renew.html",
    //                 },

    //                PostLogoutRedirectUris = { webClient },
    //                AllowedCorsOrigins = { webClient },
    //                AllowedScopes = {  IdentityServerConstants.StandardScopes.OpenId,
    //                                   IdentityServerConstants.StandardScopes.Profile,
    //                                   "backend" }

    //            }
    //        };
    //    }

    //}

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

                new ApiResource("backend", "MarketPlace REST API"),
            };
        }
        public static IEnumerable<ApiScope> ApiScopes =>
                new ApiScope[]
                {
                    new ApiScope("backend", "My API"),
                };

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