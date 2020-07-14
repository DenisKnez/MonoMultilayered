using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Project.DAL
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<Scope> ApiScopes =>
            new List<Scope>
            {
                new Scope()
            };

        public static IEnumerable<IdentityServer4.Models.Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    AllowAccessTokensViaBrowser = true,

                    ClientId = "myclient",
                    ClientSecrets = { new Secret("secret") },

                    AllowedGrantTypes = GrantTypes.Implicit,

                     // where to redirect to after login
                    //RedirectUris = { "https://localhost:5002/signin-oidc" },

                    //// where to redirect to after logout
                    //PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },

                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },

                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                }
            };
    }
}