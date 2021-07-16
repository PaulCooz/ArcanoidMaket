using System;
using Dataers;
using Libs;
using Loaders;
using Logics;
using Logics.Loaders;
using TMPro;
using UnityEngine;

namespace View
{
    public class LevelInfo : MonoBehaviour
    {
        private string _startText; 
        
        [SerializeField] 
        private TextMeshProUGUI textMeshProUGUI;

        private void Start()
        {
            _startText = textMeshProUGUI.text;
            
            EventsAndStates.OnGameStart += SetInfo;
        }

        private void SetInfo(LevelData levelData)
        {
            var info = LevelLoader.GetLevelInfo();
            
            textMeshProUGUI.text = string.Format(_startText, info.x, info.y);
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameStart -= SetInfo;
        }
    }
}
