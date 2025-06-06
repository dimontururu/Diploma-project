using Microsoft.Extensions.DependencyInjection;
using TaskSlayerfrontendTGBot;
using TaskSlayerfrontendTGBot.ApiClient;
using TaskSlayerfrontendTGBot.BotHandles.Handles;
using TaskSlayerfrontendTGBot.BotHandles.IHandles;
using TaskSlayerfrontendTGBot.Interface.IServices;
using TaskSlayerfrontendTGBot.Services;
using Telegram.Bot;

class Program
{
    static async Task Main(string[] args)
    {
        
        var serviceProvider = new ServiceCollection()
               .AddScoped<App>()
               .AddScoped<IBotServices,BotService>()
               .AddSingleton<IUserStateService, UserStateService>()
               .AddScoped<IHandleUpdate,HandleUpdate>()
               .AddScoped<IHandelCommand,HandelCommand>()
               .AddScoped<IHandleError,HandleError>()
               .AddScoped<IHandelCallbackQuery,HandelCallbackQuery>()
               .AddSingleton<IHandleUserState,HandleUserState>()
               .AddScoped<TaskServiceApiClient>(_ => new TaskServiceApiClient(Environment.GetEnvironmentVariable("base__Url"),new HttpClient()))
               .AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(Environment.GetEnvironmentVariable("Bot__token")))
               .BuildServiceProvider();

        var app = serviceProvider.GetService<App>();

        await app.Run();

        Console.ReadLine();
    }
}
