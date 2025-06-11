using Application.Interfaces;
using Telegram.Bot;

namespace TaskSlayerfrontendTGBot.Services
{
    internal class BotService: IBotServices
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
            _botClient.StartReceiving(
                updateHandler: async (TelegramBotClient,update,CancellationToken) => 
                {
                    await _UpdateHandler.HandleAsync(update); 
                },
                errorHandler: _handleError.HandleErrorAsync
            );
        }
    }
}
