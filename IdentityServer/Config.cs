﻿    using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using System.Security.Claims;

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
                },
            new Client
                {
                    ClientId = "movies_mvc_client",
                    ClientName = "Movies MVC Web App",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:7251/signin-oidc" // this is client app port
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:7251/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
        };
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]{
            new ApiScope("movieAPI","Movie API") // قدم اول تنظیمات
        };
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] { };
        public static IEnumerable<IdentityResource> IdentityResources => 
            new IdentityResource[] { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        public static List<TestUser> TestUsers => 
            new List<TestUser> { 
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username =  "mehmet",
                    Password = "mehmet",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName,"mehmet"),
                        new Claim(JwtClaimTypes.FamilyName,"ozkaya"),
                    }
                }
            };
    }
}
