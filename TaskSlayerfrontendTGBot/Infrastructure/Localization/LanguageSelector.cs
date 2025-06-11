using Application.Interfaces.Localizer;

namespace Infrastructure.Localization
{
    internal class LanguageSelector : ILanguageSelector
    {
        private static readonly Dictionary<string, string> _map = new(StringComparer.OrdinalIgnoreCase)
        {
            // English
            { "english", "en" }, { "eng", "en" }, { "en", "en" }, { "английский", "en" }, { "ағылшын", "en" },
            { "англ", "en" }, { "англ.", "en" }, { "🇬🇧 English", "en" }, { "🇬🇧 Ағылшын тілі", "en" }, { "🇬🇧 Английский", "en" },

            // Russian
            { "русский", "ru" }, { "rus", "ru" }, { "ru", "ru" }, { "russian", "ru" }, { "рус", "ru" },
            { "рус.", "ru" }, { "орыс", "ru" }, { "🇷🇺 Russian", "ru" }, { "🇷🇺 Орыс тілі", "ru" }, { "🇷🇺 Русский", "ru" },

            // Kazakh
            { "қазақ", "kk-kz" }, { "каз", "kk-kz" }, { "kz", "kk-kz" }, { "kk", "kk-kz" }, { "kaz", "kk-kz" },
            { "kazakh", "kk-kz" }, { "казахский", "kk-kz" }, { "каз.", "kk-kz" }, { "🇰🇿 Kazakh", "kk-kz" }, { "🇰🇿 Қазақ тілі", "kk-kz" },
            { "🇰🇿 Казахский", "kk-kz" },
        };

        public string? DetectLanguage(string input)
        {
            return _map.TryGetValue(input.Trim(), out var lang) ? lang : null;
        }
    }
}
