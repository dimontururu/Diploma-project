using TaskSlayerfrontendTGBot.BotHandles.IHandles;
using TaskSlayerfrontendTGBot.Interface.IServices;
using Telegram.Bot;

namespace TaskSlayerfrontendTGBot.Services
{
    internal class BotService: IBotServices
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IHandleUpdate _handelUpdate;
        private readonly IHandleError _handleError;

        public BotService(ITelegramBotClient botClient, IHandleUpdate handelUpdate,IHandleError handleError)
        {
            _botClient = botClient;
            _handelUpdate = handelUpdate;
            _handleError = handleError;
        }

        public async Task Start()
        {
            _botClient.StartReceiving(
                updateHandler: _handelUpdate.HandleUpdateAsync,
                errorHandler: _handleError.HandleErrorAsync
            );
        }
    }
}
