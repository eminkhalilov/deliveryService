using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<Client> Clients =>
      new List<Client>
      {
        new Client
        {
            ClientId = "admin_client_id",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets =
            {
                new Secret("adminfarfetchcsecret".Sha256())
            },
            AllowedScopes = { "deliveryservice" },
            ClientClaimsPrefix = "",
            Claims = new Claim[]
            {
             new Claim(ClaimTypes.Role, "Admin")
            }
        },
        new Client
        {
            ClientId = "user_client_id",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets =
            {
                new Secret("userfarfetchcsecret".Sha256())
            },
            AllowedScopes = { "deliveryservice" }
        }
      };


        public static IEnumerable<ApiResource> Apis =>
        new List<ApiResource>
        {
            new ApiResource("deliveryservice", "Delivery Service")
        };
    }
}