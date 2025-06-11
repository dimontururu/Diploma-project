using Application.Interfaces.Localizer;
using System.Globalization;
using System.Resources;

namespace Infrastructure.Localization
{
    public class ResxLocalizer : ILocalizer
    {
        private readonly ResourceManager _resourceManager;
        private readonly CultureInfo _culture;

        public ResxLocalizer(string languageCode)
        {
            _resourceManager = new ResourceManager("Infrastructure.Localization.Messages", typeof(ResxLocalizer).Assembly);
            _culture = new CultureInfo(languageCode);
        }

        public string this[string key] => (_resourceManager.GetString(key, _culture) ?? $"[{key}]").Replace(@"\n", Environment.NewLine);
    }
}
