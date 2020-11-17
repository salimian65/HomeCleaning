using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using Microsoft.Extensions.Configuration;
using IdentityServer4;

namespace HomeCleaning.ApiAndAuth
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
                new IdentityResources.Email(),
            };
        }


        public IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("backend", "MarketPlace REST API"){ UserClaims = { JwtClaimTypes.Name,
                                                                                   JwtClaimTypes.Role, 
                                                                                   ClaimTypes.Role } }
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

                    FrontChannelLogoutUri = webClient,
                    PostLogoutRedirectUris = { webClient },
                    AllowedCorsOrigins = { webClient },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "backend" }
                },

                //new Client
                //{
                //    ClientId = "client",
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,

                //    ClientSecrets =
                //    {
                //        new Secret("sssss".Sha256())
                //    },

                //    AllowedScopes = { "backend" }
                //},
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