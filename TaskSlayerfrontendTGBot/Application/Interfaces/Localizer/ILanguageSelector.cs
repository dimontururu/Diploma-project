namespace Application.Interfaces.Localizer
{
    public interface ILanguageSelector
    {
        string? DetectLanguage(string input);
    }
}
