using Microsoft.Extensions.DependencyInjection;
using task_service.Domain.Interfaces.IRepository;
using task_service.Infrastructure.Data;
using task_service.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace task_service.Infrastructure
{
    public static class RegistrationInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.
                AddScoped<IUserRepository, UserRepository>().
                AddScoped<IIdClientRepository, IdClientRepository>().
                AddScoped<IClientTypeRepository, ClientTypeRepository>().
                AddDbContext<ToDoListContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString"))); ;
            return services;
        }
    }
}
