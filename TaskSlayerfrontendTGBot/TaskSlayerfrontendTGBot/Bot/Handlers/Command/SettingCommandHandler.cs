using Application.Interfaces;
using Application.Interfaces.Message;
using Infrastructure.Localization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Presentation.Bot.Handlers.Command
{
    internal class SettingCommandHandler : IMessageHandler
    {
        private readonly ITelegramBotClient _bot;
        private readonly ISessionService _sessionService;

        public SettingCommandHandler(ITelegramBotClient bot, ISessionService sessionService)
        {
            _bot = bot;
            _sessionService = sessionService;
        }

        public async Task<bool> CanHandle(Update update)
        {
            if (update.Message?.Text == null) return false;

            var chatId = update.Message.Chat.Id;
            var localization = new ResxLocalizer(_sessionService.GetLanguage(chatId));

            return update.Message?.Text == localization["ButtonSetting"];
        }

        public async Task HandleAsync(Update update)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var user = message.From;

            var localization = new ResxLocalizer(_sessionService.GetLanguage(chatId));

            var replyKeyboard = new ReplyKeyboardMarkup
                (new[]{
                    new KeyboardButton[] {localization["ButtonLanguage"] },
                })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };


            await _bot.SendMessage(
                chatId: chatId,
                text: localization["WhatWouldYouChange"],
                replyMarkup: replyKeyboard
            );
        }
    }
}
