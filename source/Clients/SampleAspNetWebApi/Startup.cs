﻿using Microsoft.Owin;
using Owin;
using System.IdentityModel.Tokens;
using Thinktecture.IdentityModel.Owin.ScopeValidation;
using Thinktecture.IdentityModel.Tokens;
using Thinktecture.IdentityServer.v3.AccessTokenValidation;

[assembly: OwinStartup(typeof(SampleAspNetWebApi.Startup))]

namespace SampleAspNetWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = ClaimMappings.None;

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = "https://localhost:44333/core",
                });

            // require read OR write scope
            app.RequireScopes(new ScopeValidationOptions
                {
                    AllowAnonymousAccess = true,
                    Scopes = new[] { "read", "write" }
                });
            
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}