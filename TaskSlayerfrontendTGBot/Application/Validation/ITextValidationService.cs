namespace Application.Validation
{
    public interface ITextValidationService
    {
        bool ContainsInvalidCharacters(string input);
        bool IsValidLength(string input, int maxLength);
        bool IsValid(string input, int maxLength);
    }
}
