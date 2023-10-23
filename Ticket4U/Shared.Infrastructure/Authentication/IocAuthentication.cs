using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Infrastructure.Exceptions;
using System.Text;

namespace Shared.Infrastructure.Authentication;

public static class IocAuthentication
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };

            o.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    //c.NoResult();
                    //c.Response.StatusCode = 500;
                    //c.Response.ContentType = "text/plain";
                    //return c.Response.WriteAsync(c.Exception.ToString());
                    throw new Exception(c.Exception.ToString());
                },
                OnChallenge = context =>
                {
                    //context.HandleResponse();
                    //context.Response.StatusCode = 401;
                    //context.Response.ContentType = "application/json";
                    //var result = JsonSerializer.Serialize("401 Not authorized");
                    //return context.Response.WriteAsync(result);
                    throw new UserNotAuthorizedException();
                },
                OnForbidden = context =>
                {
                    //context.Response.StatusCode = 403;
                    //context.Response.ContentType = "application/json";
                    //var result = JsonSerializer.Serialize("403 Not authorized");
                    //return context.Response.WriteAsync(result);
                    throw new UserForbiddenException();
                }
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdmin", policy =>
            {
                policy.RequireRole("Admin");
            });
        });

        return services;
    }
}
