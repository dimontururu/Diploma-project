using Application.Bot;
using Application.Interfaces.Localizer;
using Application.Interfaces.Message;
using Application.Services.Domain;
using Application.Session;
using Infrastructure.Localization;
using Telegram.Bot.Types;

namespace Presentation.Bot.Handlers.Command
{
    internal class AwardCommandHandler : IMessageHandler
    {
        private readonly ISessionService _sessionService;
        private readonly ILanguageSelector _languageSelector;
        private readonly ITelegramMessageService _messageService;
        private readonly IAwardService _awardService;

        public AwardCommandHandler(ISessionService sessionService, ILanguageSelector languageSelector, ITelegramMessageService messageService, IAwardService awardService)
        {
            _sessionService = sessionService;
            _languageSelector = languageSelector;
            _messageService = messageService;
            _awardService = awardService;
        }

        public async Task<bool> CanHandle(Update update)
        {
            if (update.Message?.Text == null) return false;

            var userId = update.Message.From.Id;
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return update.Message?.Text == localization["ButtonAward"] || _languageSelector.DetectAward(update.Message?.Text);
        }

        public async Task HandleAsync(Update update)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;
            var userId = user.Id;

            var awards = await _awardService.GetUserAwarads(user);
            if (awards.Count == 0)
            {
                await _messageService.SendNoAwardsMessage(userId, chatId);
            }
            else
            {
                await _messageService.SendAwardsMessage(userId, chatId,awards);
            }
                

            Console.WriteLine($"Пользовыатель: {user.Username} (id: {user.Id}) вызвал команду Award");
        }
    }
}
