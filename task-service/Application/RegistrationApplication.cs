using Microsoft.Extensions.DependencyInjection;
using task_service.Application.Interfaces.Services;
using task_service.Application.Services;

namespace task_service.Application
{
    public static class RegistrationApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>().
                AddTransient<IToDoListService, ToDoListService>();
            return services;
        }
    }
}
