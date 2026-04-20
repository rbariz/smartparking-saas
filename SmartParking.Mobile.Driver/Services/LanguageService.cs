using System.Globalization;

namespace SmartParking.Mobile.Driver.Services
{
    public sealed class LanguageService
    {
        private const string LanguageKey = "app_language";

        public string CurrentLanguage =>
            Preferences.Default.Get(LanguageKey, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);

        public void SetLanguage(string languageCode)
        {
            if (string.IsNullOrWhiteSpace(languageCode))
                return;

            var culture = new CultureInfo(languageCode);

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Preferences.Default.Set(LanguageKey, languageCode);
        }

        public void Initialize()
        {
            var languageCode = Preferences.Default.Get(LanguageKey, "fr");
            var culture = new CultureInfo(languageCode);

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }

}
