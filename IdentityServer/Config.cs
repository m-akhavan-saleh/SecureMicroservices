using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    /// <summary>
    /// کلاس مربوط به تنظیمات
    /// Identity Server 4
    /// </summary>
    public class Config
    {
        public static IEnumerable<Client> Clients => new Client[]{
        new Client // قدم دوم تنظیمات
            {
                ClientId = "movieClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "movieAPI" }
            }
        };
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]{
            new ApiScope("movieAPI","Movie API") // قدم اول تنظیمات
        };
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] { };
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] { };
        public static List<TestUser> TestUsers => new List<TestUser> { };
    }
}
