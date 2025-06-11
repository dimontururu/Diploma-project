using Application.Interfaces;
using Application.Interfaces.Message;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Bot;
using Presentation.Bot.Handlers;
using Presentation.Bot.Handlers.Command;
using TaskSlayerfrontendTGBot;
using TaskSlayerfrontendTGBot.Services;
using Telegram.Bot;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
               .AddScoped<App>()
               .AddScoped<IBotServices, BotService>()
               .AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(Environment.GetEnvironmentVariable("Bot__token")))
               .AddScoped<IBotUpdateHandler, BotUpdateHandler>()
               .AddScoped<IHandleError, HandleError>()
               .AddScoped<IMessageHandler,StartCommandHandler>()
               .AddScoped<IMessageHandler,SettingCommandHandler>()
               .AddScoped<IMessageHandler, LanguageCommandHandler>()
               .AddScoped<IMessageHandler, BotAddedToChatHandler>()
               .InfrastructureADD()
               //.AddSingleton<IUserStateService, UserStateService>()
               //.AddScoped<IHandleUpdate,HandleUpdate>()
               //.AddScoped<IHandelCommand,HandelCommand>()
               //.AddScoped<IHandelCallbackQuery,HandelCallbackQuery>()
               //.AddSingleton<IHandleUserState,HandleUserState>()
               //.AddScoped<TaskServiceApiClient>(_ => new TaskServiceApiClient(Environment.GetEnvironmentVariable("base__Url"),new HttpClient()))
               .BuildServiceProvider();

        var app = serviceProvider.GetService<App>();

        await app.Run();

        Console.ReadLine();
    }
}
