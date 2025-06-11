using Application.Interfaces;
using Application.Interfaces.Message;
using Telegram.Bot.Types;

namespace Presentation.Bot
{
    internal class BotUpdateHandler : IBotUpdateHandler
    {
        private readonly IEnumerable<IMessageHandler> _handlers;
        private readonly ISessionService _sessionService;

        public BotUpdateHandler(IEnumerable<IMessageHandler> handlers, ISessionService sessionService)
        {
            _handlers = handlers;
            _sessionService = sessionService;
        }

        public async Task HandleAsync(Update update)
        {
            var userId = update.Message?.From?.Id ?? update.CallbackQuery?.From?.Id;


            if (userId != null)
            {
                string state = _sessionService.GetState(userId.Value);
                if (state != null)
                {
                    foreach (var handler in _handlers.OfType<IStatefulMessageHandle>())
                    {
                        if (await handler.CanHandle(state, update))
                        {
                            await handler.HandleAsync(update);
                            return;
                        }
                    }
                }
            }

            foreach (var handler in _handlers)
            {
                if (await handler.CanHandle(update))
                {
                    await handler.HandleAsync(update);
                    break;
                }
            }
        }
    }
}
