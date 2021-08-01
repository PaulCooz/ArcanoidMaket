using System;
using TMPro;
using UnityEngine;

namespace Models.Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI mainTitle;

        public void Start()
        {
            SetMainText();
        }

        public void ResetLanguage(int locale)
        {
            EventsAndStates.OnChangeLocale += SetMainText;
            LocaleManager.SetLocale((Locale) locale);
        }

        private void SetMainText()
        {
            mainTitle.text = LocaleManager.GetText("mainTitle");
        }
    }
}
