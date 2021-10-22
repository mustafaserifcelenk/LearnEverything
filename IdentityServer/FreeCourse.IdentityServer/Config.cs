// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        // Aud'lara karşılık geliyor.
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("photo_stock_catalog"){Scopes={"photo_stock_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        // Burada email ve password gönderildiğinde kullanıcıya dair hangi bilgilerin client tarafından elde edileceğini belirliyoruz
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(), // token almak için zorunlu alan, subject id olarak geçer
                       new IdentityResources.Profile(),
                       // Yukarıda yazılan 3'ününde claimleri default vardırlar ancak bu role olanı biz yazdığımız için bunun claim'ini manuel olarak mapledik
                       new IdentityResource(){Name="roles",DisplayName="Roles",Description="Kullanıcı Rolleri", UserClaims=new []{ "role" } }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission", "Catalog API için full erişim"),
                //new ApiScope("catalog_write", "Catalog API için full erişim"), bunun gibi farklı farklı izinler olabilir
                new ApiScope("photo_stock_fullpermission", "Photo Stock API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId ="WebMvcClient",
                    ClientName = "Asp.Net Core MVC",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={"catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName}
                },
                new Client
                {
                    ClientId ="WebMvcClientForUser",
                    ClientName = "Asp.Net Core MVC",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess},
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    // Refresh Token eğer kullanıcı offline'sa token'ın 60 güne kadar kullanılmasını sağlayan özellik
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage=TokenUsage.ReUse
                }
            };
    }
}