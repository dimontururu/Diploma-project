using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using task_service.Application.Interfaces;
using task_service.Domain.Interfaces.IRepository;
using task_service.Infrastructure.Data;
using task_service.Infrastructure.Data.Repositories;
using task_service.Infrastructure.JWT;

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
                AddScoped<IToDoListRepository, ToDoListRepository>().
                AddScoped<ICaseRepository, CaseRepository>().
                AddDbContext<ToDoListContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString"))).
                AddTransient<ITokenService, JwtService>(); ;

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
                        ValidIssuer = Environment.GetEnvironmentVariable("JWT__ISSUER"),
                        ValidAudience = Environment.GetEnvironmentVariable("JWT__AUDIENCE"),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSecret))
                    };
                });
            return services;
        }
    }
}
