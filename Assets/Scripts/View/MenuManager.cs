using System;
using Loaders;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI mainTitle;

    public void ResetLanguage(int locale)
    {
        LocaleManager.SetLocale((Locale) locale);
    }

    private void Start()
    {
        mainTitle.text = LocaleManager.GetText("mainTitle");
    }
}
