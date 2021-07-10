using Libs;
using Loaders;
using TMPro;
using UnityEngine;

namespace View
{
    public class PausePopup : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI pauseTitle;
        
        private void OnEnable()
        {
            pauseTitle.text = LocaleManager.GetText("pauseTitle");
            
            EventsAndStates.IsGameRun = false;
        }

        private void OnDisable()
        {
            EventsAndStates.IsGameRun = true;
        }
    }
}