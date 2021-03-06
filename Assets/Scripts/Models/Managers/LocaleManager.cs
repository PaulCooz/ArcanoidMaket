using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Models.Managers
{
    public enum Locale
    {
        En,
        Ru
    }
    
    public static class LocaleManager
    {
        private static readonly JToken Texts = JToken.Parse(Resources.Load<TextAsset>("Locales").text);
        private static Locale _locale = Locale.En;

        public static void SetLocale(Locale locale)
        {
            _locale = locale;
            EventsAndStates.ChangeLocale();
        }

        public static string GetText(string textId)
        {
            return (string) Texts?[textId]?[(int) _locale];
        }
    }
}
