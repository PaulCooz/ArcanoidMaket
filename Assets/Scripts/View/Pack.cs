using Dataers;
using Models.Managers;
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
        [SerializeField] 
        private Foreground foreground;
        [SerializeField] 
        private float sceneSwapDuration = 1;

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
            if (PlayerEnergy.Energy < 1) return;

            var currentPack = PlayerData.GetLastPack();
            if (currentPack < packNumber) return;

            DataHolder.SetLevelPack(packLevels, packNumber, packImage);
            
            foreground.Show(sceneSwapDuration);
            StartCoroutine(SceneChanger.WaitAndChange("Game", sceneSwapDuration));
        }
    }
}
