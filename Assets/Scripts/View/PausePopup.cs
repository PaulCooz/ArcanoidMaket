using System;
using Libs;
using Loaders;
using TMPro;
using UnityEngine;

public class PausePopup : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI pauseTitle;

    private void Start()
    {
        pauseTitle.text = LocaleManager.GetText("pauseTitle");
    }

    private void OnEnable()
    {
        EventsAndStates.IsGameRun = false;
    }

    private void OnDisable()
    {
        EventsAndStates.IsGameRun = true;
    }
}