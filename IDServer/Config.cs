using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IDServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
                // You may add other identity resources like address,phone... etc
                //new IdentityResources.Address()
            };
        }

        // Block 1: All APIs, I want to protect in my system
        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("User", "UserV1"),
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(name: "User",   displayName: "UserV1"),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                //Block 2:  MVC client using hybrid flow
                //new Client
                //{
                //    ClientId = "webclient",
                //    ClientName = "Web Client",
                //    RequireConsent = false,
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    RequireClientSecret = false,

                //    RedirectUris = { "https://localhost:44373/api/v2/User/list","https://localhost:44373/api/v2/User/","https://localhost:44373/api/v1/User/create","https://localhost:44373/api/v1/User/delete" },
                //    //FrontChannelLogoutUri = "https://localhost:44373/error",
                //    //PostLogoutRedirectUris = { "https://localhost:44373/error" },
                //    AllowAccessTokensViaBrowser=true,
                //    AllowOfflineAccess = true,
                //    AllowedScopes = {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //    }                
                //},
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client1",
                    ClientSecrets = { new Secret(("secret").Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "userApi1" }
                },
                    new Client
                    {
                         ClientName = "Api",
                         ClientId = "MyClient",
                         AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                         RequireClientSecret = false,
                         AllowedScopes = { "User" },
                    },
                    new Client
                    {
                    }

                //Block 3: SPA client using Code flow
                //new Client
                //{
                //    ClientId = "spaclient",
                //    ClientName = "SPA Client",
                //    ClientUri = "https://localhost:5003",
                //    RequireConsent = false,
                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequirePkce = true,
                //    RequireClientSecret = false,
                //    AllowAccessTokensViaBrowser = true,

                //    RedirectUris =
                //    {
                //        "https://localhost:5003/index.html",
                //        "https://localhost:5003/callback.html"
                //    },

                //    PostLogoutRedirectUris = { "https://localhost:5003/index.html" },
                //    AllowedCorsOrigins = { "https://localhost:5003" },

                //    AllowedScopes = { "openid", "profile", "identity.api" ,"test.api" }
                //}
            };
        }
    }
}
