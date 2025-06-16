namespace Application.Interfaces.Localizer
{
    public interface ILanguageSelector
    {
        string? DetectLanguage(string input);
        public bool DetectMenu(string input);
        bool DetectSetting(string input);
        bool DetectLanguageCommand(string input);
        bool DetectToDoList(string input);
        bool DetectADDToDoList(string input);
        bool DetectDeleteToDoList(string input);
        bool DetectAward(string input);
    }
}
