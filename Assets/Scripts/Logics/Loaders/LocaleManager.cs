using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Logics.Loaders
{
    public enum Locale
    {
        En,
        Ru
    }
    
    public class LocaleManager : MonoBehaviour
    {
        private JToken _texts;
        private Locale _locale;
        
        [SerializeField]
        private TextAsset source;
    
        private void Awake()
        {
            _locale = Locale.En;
            _texts = JToken.Parse(source.text);
        }

        public void SetLocale(Locale locale)
        {
            _locale = locale;
        }

        public string GetText(string textId)
        {
            return (string) _texts?[textId]?[(int) _locale];
        }
    }
}
