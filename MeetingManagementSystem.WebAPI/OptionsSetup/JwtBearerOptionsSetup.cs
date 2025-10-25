using MeetingManagementSystem.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MeetingManagementSystem.WebAPI.OptionsSetup
{
    public sealed class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly JwtOptions _jwtOptions;

        public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public void PostConfigure(string? name, JwtBearerOptions options)
        {
            options.TokenValidationParameters.ValidateIssuer = true;
            options.TokenValidationParameters.ValidateAudience = true;
            options.TokenValidationParameters.ValidateLifetime = true;
            options.TokenValidationParameters.ValidateIssuerSigningKey = true;
            options.TokenValidationParameters.ValidIssuer = _jwtOptions.Issuer;
            options.TokenValidationParameters.ValidAudience = _jwtOptions.Audience;
            options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

           
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;

                    if (!string.IsNullOrEmpty(accessToken) &&
                        path.StartsWithSegments("/chatHub"))
                    {
                    }

                    return Task.CompletedTask;
                }
            };
        }
    }
}
