using Controllers.Managers;
using Dataers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Pack : MonoBehaviour
    {
        private string _packProgressText;
        
        [SerializeField]
        private TextAsset[] packLevels;
        [SerializeField] 
        private TextMeshProUGUI packTitle;
        [SerializeField] 
        private float imageRatio;
        [SerializeField]
        private int packNumber;
        [SerializeField] 
        private Image image;
        [SerializeField] 
        private Image packImage;
        [SerializeField] 
        private TextMeshProUGUI packProgress;

        private void Start()
        {
            packTitle.text = LocaleManager.GetText("packName" + packNumber);
            _packProgressText = packProgress.text;

            SetProgress();
        }

        private void SetProgress()
        {
            var currentPack = PlayerData.GetLastPack();
            int currentLevel;
            
            if (currentPack < packNumber)
            {
                image.color *= imageRatio;
                currentLevel = 0;
            }
            else if (currentPack > packNumber)
            {
                currentLevel = packLevels.Length;
            }
            else
            {
                currentLevel = PlayerData.GetLastLevel();
            }
            
            packProgress.text = string.Format(_packProgressText, currentLevel, packLevels.Length);
        }

        public void Pushed()
        {
            var currentPack = PlayerData.GetLastPack();
            if (currentPack < packNumber) return;
            
            DataHolder.SetLevelPack(packLevels, packNumber, packImage);
            SceneChanger.LoadScene("game");
        }
    }
}
