using Application.Bot;
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
        Console.Clear();
        var serviceProvider = new ServiceCollection()
               .AddScoped<App>()
               .AddScoped<IBotService, BotService>()
               .AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(Environment.GetEnvironmentVariable("Bot__token")))
               .AddScoped<IBotUpdateHandler, BotUpdateHandler>()
               .AddScoped<IHandleError, HandleError>()
               .AddScoped<IMessageHandler,StartCommandHandler>()
               .AddScoped<IMessageHandler,SettingCommandHandler>()
               .AddScoped<IMessageHandler, LanguageCommandHandler>()
               .AddScoped<IMessageHandler, BotAddedToChatHandler>()
               .AddScoped<IMessageHandler, MenuCommandHandler>()
               .AddScoped<IMessageHandler, HelpCommandHandler>()
               .AddScoped<IMessageHandler, ToDoListCommandHandler>()
               .AddScoped<IMessageHandler, ADDToDoListCommandHandler>()
               .AddScoped<IMessageHandler,DeleteToDoListCommandHandler>()
               .AddScoped<IMessageHandler, InlineKeyboardButtonCommandHandler>()
               .AddScoped<IMessageHandler, EditToDoListCommandHandler>()
               .AddScoped<IMessageHandler, ViewToDoListCommandHandler>()
               .AddScoped<IMessageHandler, ADDCaseCommandHandler>()
               .AddScoped<IMessageHandler, DeleteCaseCommandHandler>()
               .AddScoped<IMessageHandler, EditCaseCommandHandler>()
               .AddScoped<IMessageHandler, AwardCommandHandler>()
               .AddSingleton<HttpClient>()
               .InfrastructureADD()
               .BuildServiceProvider();

        var app = serviceProvider.GetService<App>();

        await app.Run();
    }
}
