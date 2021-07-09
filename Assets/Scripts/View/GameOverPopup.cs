using Loaders;
using TMPro;
using UnityEngine;

namespace View
{
    public class GameOverPopup : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI titleText;

        private void Start()
        {
            titleText.text = LocaleManager.GetText("gameOverPopupTitle");
        }
    }
}
