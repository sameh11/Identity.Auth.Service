using IdentityServer4.Models;
using System.Security.Claims;

namespace Identity.Auth.Web
{
    public static class Config
    {
        public static List<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "Administration-Parent-Client",
                ClientName = "Administration-Parent-Client",
                ClientUri = "https://localhost:7064",
                ClientSecrets = { new Secret("49C1A7E1-0C794A89A3D6A37998FB86B0".Sha256()) },
                AllowedGrantTypes = { "password", "authorization_code","client_credentials" } ,

                RequireClientSecret = false,
                RequireConsent = false,
                RedirectUris = { "https://localhost:7064/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:7064/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7064" },
                BackChannelLogoutUri = "https://localhost:7064" ,
                AllowOfflineAccess = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowedScopes = {
                    "administration-parent-client",
                    "parent.write",
                    "parent.read",
                    "child.write",
                    "child.read",
                    "openid",
                    "profile",
                    "roles",
                },
            }
        };

        public static List<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource
            {
                Name = "Administration-Parent-Api",
                DisplayName = "Adminsration parent api",
                Scopes = new List<string>
                {
                    "parent.write",
                    "parent.read",
                    "child.write",
                    "child.read"
                },
            }
        };

        public static IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
        {
            new ApiScope("parent.write"),
            new ApiScope("parent.read"),
            new ApiScope("child.write"),
            new ApiScope("child.read"),
            new ApiScope("openid"),
            new ApiScope("profile"),
            new ApiScope("email"),
            new ApiScope("read"),
            new ApiScope("write"),
            new ApiScope("roles"),
            new ApiScope("Administration-Parent-Api")
        };
    }
}
