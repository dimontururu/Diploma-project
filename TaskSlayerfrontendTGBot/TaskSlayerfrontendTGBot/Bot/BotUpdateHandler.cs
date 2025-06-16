using Application.Bot;
using Application.Interfaces.Message;
using Application.Session;
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
            try
            {
                var userId = update.Message?.From?.Id ?? update.CallbackQuery?.From?.Id;

                foreach (var handler in _handlers)
                {
                    if (await handler.CanHandle(update))
                    {
                        if (userId != null)
                        {
                            _sessionService.ClearState(userId.Value);
                        }
                        await handler.HandleAsync(update);
                        return;
                    }
                }

                if (userId != null)
                {
                    string state = _sessionService.GetState(userId.Value);
                    if (state != null || update.CallbackQuery != null)
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
            }
            catch { }
        }
    }
}
