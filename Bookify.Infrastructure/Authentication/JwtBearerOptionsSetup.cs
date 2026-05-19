using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication
{
    internal sealed class JwtBearerOptionsSetup(IOptions<AuthenticationOptions> authenticationOptions) : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly AuthenticationOptions _authenticationOptions = authenticationOptions.Value;

        public void Configure(JwtBearerOptions options)
        {
            options.MapInboundClaims = false;

            options.Audience = _authenticationOptions.Audience; 
            options.MetadataAddress = _authenticationOptions.MetaDataUrl;
            options.RequireHttpsMetadata = _authenticationOptions.RequireHttpsMetadata;
            options.TokenValidationParameters.ValidIssuer = _authenticationOptions.Issuer;  
        }

        public void Configure(string? name,JwtBearerOptions options)
        {
            Configure(options);
        }
    }
}