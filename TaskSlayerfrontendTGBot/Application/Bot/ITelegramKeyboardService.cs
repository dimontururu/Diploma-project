using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Bot
{
    public interface ITelegramKeyboardService
    {
        ReplyKeyboardMarkup GetMainMenuKeyboard(long userId);
        ReplyKeyboardMarkup GetAddToDoAndMenuKeyboard(long userId);
        ReplyKeyboardMarkup GetToDoActionsKeyboard(long userId);
        ReplyKeyboardMarkup GetMenuKeyboard(long userId);
        ReplyKeyboardMarkup GetLanguage(long userId);
        ReplyKeyboardMarkup GetLanguageKeyboard(long userId);
        InlineKeyboardMarkup GetSettingToDoListKeyboard(long userId, string idToDoList);
        ReplyKeyboardMarkup GetAddCaseMenuKeyboard(long userId);
        ReplyKeyboardMarkup GetCaseActionsKeyboard(long userId);
        InlineKeyboardMarkup GetSettingCaseKeyboard(long userId, string idCase, string idToDoList);
        InlineKeyboardMarkup GetSettingTrueCaseKeyboard(long userId, string idCase);
    }
}
