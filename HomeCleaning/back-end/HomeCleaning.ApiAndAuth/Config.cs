using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using Microsoft.Extensions.Configuration;
using IdentityServer4;

namespace HomeCleaning.ApiAndAuth
{

    //public class Config
    //{
    //    public static IEnumerable<Client> Clients = new List<Client>
    //    {
    //        new Client
    //        {
    //            ClientId = "frontend",
    //            AllowedGrantTypes = GrantTypes.Code,
    //            RequireClientSecret = false,
    //            RequirePkce = true,
    //            RequireConsent = false,
    //            RedirectUris = {
    //                "http://localhost:8080/callback",
    //                "http://localhost:8080/static/silent-renew.html"
    //            },
    //            PostLogoutRedirectUris = { "http://localhost:8080" },
    //            AllowedScopes = { "openid", "profile", "email", IdentityServerConstants.LocalApi.ScopeName },
    //            AllowedCorsOrigins = { "http://localhost:8080" }
    //        },
    //    };

    //    public static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource>
    //    {
    //        new IdentityResources.OpenId(),
    //        new IdentityResources.Profile(),
    //        new IdentityResources.Email(),
    //    };

    //    public static IEnumerable<ApiResource> Apis = new List<ApiResource>
    //    {
    //        // local API
    //        new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
    //    };
    //}


    public class Config
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
                        "backend"
                    })
                {
                    Scopes = { "backend" }
                },
            };
        }
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("backend", "My API",
                    new []
                    {
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Name,
                        "backend"
                    })
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
                    AccessTokenLifetime = 3600 , // 1.5 minutes
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


    //public static class Config
    //{
    //    public static IEnumerable<IdentityResource> IdentityResources =>
    //               new IdentityResource[]
    //               {
    //            new IdentityResources.OpenId(),
    //            new IdentityResources.Profile(),
    //               };

    //    public static IEnumerable<ApiScope> ApiScopes =>
    //        new ApiScope[]
    //        {
    //            new ApiScope("scope1"),
    //            new ApiScope("scope2"),
    //        };

    //    public static IEnumerable<Client> Clients =>
    //        new Client[]
    //        {
    //            // m2m client credentials flow client
    //            new Client
    //            {
    //                ClientId = "m2m.client",
    //                ClientName = "Client Credentials Client",

    //                AllowedGrantTypes = GrantTypes.ClientCredentials,
    //                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

    //                AllowedScopes = { "scope1" }
    //            },

    //            // interactive client using code flow + pkce
    //            new Client
    //            {
    //                ClientId = "interactive",
    //                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

    //                AllowedGrantTypes = GrantTypes.Code,

    //                RedirectUris = { "https://localhost:44300/signin-oidc" },
    //                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
    //                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

    //                AllowOfflineAccess = true,
    //                AllowedScopes = { "openid", "profile", "scope2" }
    //            },
    //        };
    //}

    //........................................

    //public IEnumerable<IdentityResource> GetIdentityResources()
    //{
    //    return new IdentityResource[]
    //    {
    //            new IdentityResources.OpenId(),
    //            new IdentityResources.Profile(),
    //            new IdentityResources.Email(),
    //    };
    //}


    //public IEnumerable<ApiResource> Apis = new List<ApiResource>
    //    {
    //        // local API
    //        new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
    //    };

    //public IEnumerable<Client> GetClients()
    //{
    //    var webClient = Configuration["partner:webClient"];
    //    return new[]
    //    {
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
    //                RedirectUris =
    //                {
    //                  webClient,
    //                  webClient + "/callback",
    //                  webClient + "/silent",
    //                  webClient + "/popup",
    //                },
    //                FrontChannelLogoutUri = webClient,
    //                PostLogoutRedirectUris = { webClient },
    //                AllowedCorsOrigins = {     webClient },

    //                AllowedScopes = { "openid", "profile", "backend" ,"email", IdentityServerConstants.LocalApi.ScopeName }
    //            },

    //            new Client
    //            {
    //                ClientId = "client",
    //                AllowedGrantTypes = GrantTypes.ClientCredentials,

    //                ClientSecrets =
    //                {
    //                    new Secret("sssss".Sha256())
    //                },

    //                AllowedScopes = { "backend" }
    //            },
    //        };
    //}
}