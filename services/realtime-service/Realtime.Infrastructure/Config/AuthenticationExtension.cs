using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Realtime.Infrastructure.Security;


namespace Realtime.Infrastructure.Config
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = ctx =>
                        {
                            var accessToken = ctx.Request.Query["access_token"];
                            var path = ctx.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken)
                                && path.StartsWithSegments("/realtime"))
                            {
                                ctx.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<IOptions<JwtOptions>>((options, jwt) =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwt.Value.Issuer,
                        ValidAudience = jwt.Value.Audience,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt.Value.SecretKey)
                        ),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization();
            return services;
        }

    }
}
