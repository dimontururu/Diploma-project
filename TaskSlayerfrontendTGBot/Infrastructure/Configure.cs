using Application.Bot;
using Application.Interfaces.ApiClients;
using Application.Interfaces.Localizer;
using Application.Interfaces.RepositoryApi;
using Application.Services.Domain;
using Application.Session;
using Application.Validation;
using Infrastructure.Api;
using Infrastructure.Bot;
using Infrastructure.Localization;
using Infrastructure.RepositoryApi;
using Infrastructure.service;
using Infrastructure.service.Bot;
using Infrastructure.service.Command;
using Infrastructure.service.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Configure
    {
        public static IServiceCollection InfrastructureADD(this IServiceCollection ser)
        {
            return ser.AddSingleton<ISessionService, SessionService>()
                .AddSingleton<ILanguageSelector, LanguageSelector>()
                .AddSingleton<ITaskSlayerApiClient, TaskSlayerApiClient>()
                .AddSingleton<ITelegramKeyboardService, TelegramKeyboardService>()
                .AddSingleton<IToDoListService, ToDoListService>()
                .AddSingleton<IUserErrorService, UserErrorService>()
                .AddSingleton<ITextValidationService, TextValidationService>()
                .AddSingleton<IUserErrorService, UserErrorService>()
                .AddSingleton<IRepositoryAuth, RepositoryAuth>()
                .AddSingleton<IRepositoryAward, RepositoryAward>()
                .AddSingleton<IRepositoryCase, RepositoryCase>()
                .AddSingleton<IRepositoryToDoList, RepositoryToDoList>()
                .AddSingleton<IRepositoryUser, RepositoryUser>()
                .AddSingleton<IRepositoryUserAward, RepositoryUserAward>()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<ITelegramMessageService, TelegramMessageService>()
                .AddSingleton<ICaseService, CaseService>()
                .AddSingleton<IUserApiResolver,UserApiResolver>()
                .AddSingleton<IAwardService, AwardService>();


        }
    }
}
