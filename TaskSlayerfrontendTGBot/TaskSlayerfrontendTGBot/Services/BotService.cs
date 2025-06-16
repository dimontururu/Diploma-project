using Application.Bot;
using Application.Interfaces;
using System.Threading;
using Telegram.Bot;

namespace TaskSlayerfrontendTGBot.Services
{
    internal class BotService: IBotService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IBotUpdateHandler _UpdateHandler;
        private readonly IHandleError _handleError;

        public BotService(ITelegramBotClient botClient, IBotUpdateHandler UpdateHandler,IHandleError handleError)
        {
            _botClient = botClient;
            _UpdateHandler = UpdateHandler;
            _handleError = handleError;
        }

        public async Task Start()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            _botClient.StartReceiving(
                updateHandler: async (TelegramBotClient,update,CancellationToken) => 
                {
                    await _UpdateHandler.HandleAsync(update); 
                },
                errorHandler: _handleError.HandleErrorAsync,
                cancellationToken: cancellationTokenSource.Token
            );

            await Task.Delay(Timeout.Infinite, cancellationTokenSource.Token);
        }
    }
}
