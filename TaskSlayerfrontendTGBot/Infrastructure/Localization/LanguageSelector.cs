using Application.Interfaces.Localizer;

namespace Infrastructure.Localization
{
    internal class LanguageSelector : ILanguageSelector
    {
        private static readonly Dictionary<string, string> _language = new(StringComparer.OrdinalIgnoreCase)
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
            return _language.TryGetValue(input.Trim().ToLower(), out var lang) ? lang : null;
        }

        private static readonly List<string> _menu = new List<string>()
        {
            // English
            "menu","/menu",

            // Russian
            "меню","/меню",

            // Kazakh
            "мәзір","/мәзір"
        };

        public bool DetectMenu(string input)
        {
            return _menu.Contains(input.Trim().ToLower());
        }

        private static readonly List<string> _setting = new List<string>()
        {
            // English
            "setting","/setting",

            // Russian
            "настройки","/настроойки",

            // Kazakh
            "параметрлері","/параметрлері"
        };

        public bool DetectSetting(string input)
        {
            return _setting.Contains(input.Trim().ToLower());
        }

        private static readonly List<string> _languageCommand = new List<string>()
        {
            // English
            "language","/language",

            // Russian
            "язык","/язык",

            // Kazakh
            "тіл","/тіл",
        };

        public bool DetectLanguageCommand(string input)
        {
            return _languageCommand.Contains(input.Trim().ToLower());
        }

        private static readonly List<string> _toDoList = new List<string>()
        {
            // English
            "my lists","/my lists","lists","/lists",

            // Russian
            "мои списки","/мои списки","списки","/списки",

            // Kazakh
            "менің тізімдерім","/менің тізімдерім","тізімдер","/тізімдер",
        };

        public bool DetectToDoList(string input)
        {
            return _toDoList.Contains(input.Trim().ToLower());
        }

        private static readonly List<string> _ADDToDoList = new List<string>()
        {
            // English
            "add list","/add list","new list","/new list",

            // Russian
            "добавить список","/добавить список","новый список","/новый список",

            // Kazakh
            "тізім қосу","/тізім қосу","жаңа тізім","/жаңа тізім",
        };

        public bool DetectADDToDoList(string input)
        {
            return _ADDToDoList.Contains(input.Trim().ToLower());
        }

        private static readonly List<string> _DeleteToDoList = new List<string>()
        {
            // English
            "delete list","/delete list",

            // Russian
            "удалить список","/удалить список",

            // Kazakh
            "тізімді жою","/тізімді жою",
        };

        public bool DetectDeleteToDoList(string input)
        {
            return _DeleteToDoList.Contains(input.Trim().ToLower());
        }

        private static readonly List<string> _Award = new List<string>()
        {
            // English
            "awards","/awards", "my awards","/my awards","/award","award",

            // Russian
            "награды","/награды", "мои награды","/мои награды",

            // Kazakh
            "наградалар","/наградалар", "менің марапаттарым","/менің марапаттарым",
        };

        public bool DetectAward(string input)
        {
            return _Award.Contains(input.Trim().ToLower());
        }
    }
}
