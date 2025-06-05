using TaskSlayerfrontendTGBot.BotHandles.IHandles;
using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace TaskSlayerfrontendTGBot.BotHandles.Handles
{
    internal class HandleError : IHandleError
    {
        public Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }
    }
}
