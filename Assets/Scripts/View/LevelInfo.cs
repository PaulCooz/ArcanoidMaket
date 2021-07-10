using Libs;
using Loaders;
using TMPro;
using UnityEngine;

namespace View
{
    public class LevelInfo : MonoBehaviour
    {
        private string _startText; 
        
        [SerializeField] 
        private TextMeshProUGUI textMeshProUGUI;
        [SerializeField]
        private LevelLoader levelLoader;

        private void Start()
        {
            _startText = textMeshProUGUI.text;
            
            EventsAndStates.OnGameStart += SetInfo;
        }

        private void SetInfo()
        {
            var info = levelLoader.GetLevelInfo();
            
            textMeshProUGUI.text = string.Format(_startText, info.x, info.y);
        }
    }
}
