using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("goalCalendarApi", "GoalCalendarApi")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "resourceOwner",
                    // resource owner password grant client
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("1YAueOC".Sha256())
                    },

                    // allow refresh tokens
                    AllowOfflineAccess = true,

                    // when refreshing the token, the lifetime of the refresh token will be renewed
                    RefreshTokenExpiration = TokenExpiration.Sliding,

                    // makes it so you can reuse the refresh token multiple times(better change it)
                    RefreshTokenUsage = TokenUsage.ReUse,

                    // force the access tokens to refresh in the Profile service
                    UpdateAccessTokenClaimsOnRefresh = true,

                    // scopes that client has access to
                    AllowedScopes =
                    {
                        "goalCalendarApi",
                        IdentityServerConstants.StandardScopes.Email,
                    }
                }
            };
        }
    }
}
