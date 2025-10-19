using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AutoML.Api.Extensions
{
    public static class ApiServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            var issuer = configuration["Jwt:Issuer"] ??
                throw new InvalidOperationException("Jwt:Issuer is missing in configuration.");

            var audience = configuration["Jwt:Audience"] ??
                throw new InvalidOperationException("Jwt:Audience is missing in configuration.");

            var key = configuration["Jwt:Key"] ??
                throw new InvalidOperationException("Jwt:Key is missing in configuration.");

            // Configure authentication and authorization
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanReadModels", policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim("scope", "model.read")
                        || context.User.IsInRole("Admin")
                    ));

                options.AddPolicy("CanTrainModels", policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim("scope", "model.train")
                        || context.User.IsInRole("Admin")
                    ));

                options.AddPolicy("CanWriteModels", policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim("scope", "model.write")
                        || context.User.IsInRole("Admin")
                    ));

                options.AddPolicy("CanDeleteModels", policy =>
                    policy.RequireAssertion(
                        context => context.User.HasClaim("scope", "model.delete")
                        || context.User.IsInRole("Admin")
                    ));
            });

            return services;
        }
    }
}
