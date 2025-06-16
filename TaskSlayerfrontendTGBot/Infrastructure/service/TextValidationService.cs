using Application.Validation;
using System.Text.RegularExpressions;

namespace Infrastructure.service
{
    public class TextValidationService : ITextValidationService
    {
        public bool ContainsInvalidCharacters(string input)
        {
            
            return !Regex.IsMatch(input, @"^[\p{L}\p{N}\p{P} ]+$");
        }

        public bool IsValidLength(string input, int maxLength)
        {
            return input.Length <= maxLength;
        }

        public bool IsValid(string input, int maxLength)
        {
            return !ContainsInvalidCharacters(input) && IsValidLength(input, maxLength);
        }
    }
}
