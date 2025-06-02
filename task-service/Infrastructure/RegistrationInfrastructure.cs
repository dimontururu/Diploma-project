using Microsoft.Extensions.DependencyInjection;
using task_service.Domain.Interfaces.IRepository;
using task_service.Infrastructure.Data;
using task_service.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using task_service.Application.Interfaces;
using task_service.Infrastructure.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace task_service.Infrastructure
{
    public static class RegistrationInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.
                AddScoped<IUserRepository, UserRepository>().
                AddScoped<IIdClientRepository, IdClientRepository>().
                AddScoped<IClientTypeRepository, ClientTypeRepository>().
                AddDbContext<ToDoListContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString")));

            services.Configure<JwtSettings>(options =>
            {
                options.Secret = Environment.GetEnvironmentVariable("JWT__SECRET");
                options.Issuer = Environment.GetEnvironmentVariable("JWT__ISSUER");
                options.Audience = Environment.GetEnvironmentVariable("JWT__AUDIENCE");
                options.ExpiryMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT__EXPIRY_MINUTES"));
            });

            services.AddSingleton<ITokenService, JwtService>();

            var jwtSecret = Environment.GetEnvironmentVariable("JWT__SECRET")
                ?? throw new InvalidOperationException("JWT_SECRET не настроен в переменных окружения");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSecret))
                    };
                });
            return services;
        }
    }
}
