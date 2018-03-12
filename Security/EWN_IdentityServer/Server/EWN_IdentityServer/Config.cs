using IdentityServer4.Models;
using System.Collections.Generic;

namespace EWN_IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource> { new ApiResource("api1", "My Api") };

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) }, AllowedScopes = { "api1" }
                }
            };
        }
    }
}





