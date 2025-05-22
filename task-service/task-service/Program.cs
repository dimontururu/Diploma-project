using Microsoft.EntityFrameworkCore;
using task_service.Application.Services;
using task_service.Domain.Interfaces;
using task_service.Infrastructure.Data;
using task_service.Infrastructure.Middlewares;
using task_service.Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories;

namespace task_service.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<IUserService,UserService>();
            builder.Services.AddDbContext<ToDoListContext>(options=> options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IIdClientRepository,IdClientRepository>();
            builder.Services.AddScoped<IClientTypeRepository, ClientTypeRepository>();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
