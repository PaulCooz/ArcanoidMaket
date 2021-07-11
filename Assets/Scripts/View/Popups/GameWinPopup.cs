using Loaders;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class GameWinPopup : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI titleText;
        [SerializeField]
        private Image progress;
        [SerializeField] 
        private LevelLoader levelLoader;

        private void OnEnable()
        {
            titleText.text = LocaleManager.GetText("gameWinPopupTitle");

            var info = levelLoader.GetLevelInfo();
            progress.fillAmount = info.x / info.y;
        }
    }
}
