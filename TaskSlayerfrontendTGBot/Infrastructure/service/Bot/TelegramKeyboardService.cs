using Application.Bot;
using Application.Session;
using Infrastructure.Localization;
using Telegram.Bot.Types.ReplyMarkups;

namespace Infrastructure.service.Bot
{
    internal class TelegramKeyboardService : ITelegramKeyboardService
    {
        private readonly ISessionService _sessionService;
        public TelegramKeyboardService(ISessionService sessionService) 
        { 
            _sessionService = sessionService;
        }

        public ReplyKeyboardMarkup GetMainMenuKeyboard(long userId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));
            return new ReplyKeyboardMarkup
            (new[]{
                new KeyboardButton[] {localization["ButtonToDoList"],localization["ButtonAward"] },
                new KeyboardButton[] { localization["ButtonSetting"] }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetAddToDoAndMenuKeyboard(long userId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));
            return new ReplyKeyboardMarkup
            (new[]{
                new KeyboardButton[] { localization["ButtonADDToDoList"]},
                new KeyboardButton[] { localization["ButtonMenu"] }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetAddCaseMenuKeyboard(long userId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));
            return new ReplyKeyboardMarkup
            (new[]{
                new KeyboardButton[] { localization["ButtonADDCase"]},
                new KeyboardButton[] { localization["ButtonMenu"] }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetToDoActionsKeyboard(long userId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));
            return new ReplyKeyboardMarkup
            (new[]{
                new KeyboardButton[] { localization["ButtonADDToDoList"], localization["ButtonDeleteToDoList"] },
                new KeyboardButton[] { localization["ButtonMenu"] }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetCaseActionsKeyboard(long userId) 
        { 
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));
            return new ReplyKeyboardMarkup
            (new[]{
                new KeyboardButton[] { localization["ButtonADDCase"], localization["ButtonDeleteCase"] },
                new KeyboardButton[] { localization["ButtonMenu"] }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetMenuKeyboard(long userId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { localization["ButtonMenu"] }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetLanguage(long userId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return new ReplyKeyboardMarkup(
            new[]
            {
                new KeyboardButton[]
                {
                    localization["ButtonLanguageEnglish"],
                    localization["ButtonLanguageRussian"],
                    localization["ButtonLanguageKazakh"]
                },
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public ReplyKeyboardMarkup GetLanguageKeyboard(long userId)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return new ReplyKeyboardMarkup
            (new[]{
                new KeyboardButton[] {localization["ButtonLanguage"] },
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };
        }

        public InlineKeyboardMarkup GetSettingToDoListKeyboard(long userId,string idToDoList)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(localization["ButtonViewToDoList"], $"InlineButtonViewToDoList/{idToDoList}"),
                    InlineKeyboardButton.WithCallbackData(localization["ButtonEditToDoList"], $"InlineButtonEditToDoList/{idToDoList}"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(localization["ButtonADDCase"], $"InlineButtonADDCase/{idToDoList}"),
                    InlineKeyboardButton.WithCallbackData(localization["ButtonDeleteToDoListMini"], $"InlineButtonDeleteToDoList/{idToDoList}")
                }
            });
        }

        public InlineKeyboardMarkup GetSettingCaseKeyboard(long userId, string idCase, string idToDoList)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(localization["ButtonCompleted"], $"InlineButtonEditCase/{idCase}"),
                    InlineKeyboardButton.WithCallbackData(localization["ButtonDeleteToDoListMini"], $"InlineButtonDeleteCase/{idCase}")
                }
            });
        }
        public InlineKeyboardMarkup GetSettingTrueCaseKeyboard(long userId, string idCase)
        {
            var localization = new ResxLocalizer(_sessionService.GetLanguage(userId));

            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(localization["ButtonDeleteToDoListMini"], $"InlineButtonDeleteCase/{idCase}")
                }
            });
        }
    }
}
