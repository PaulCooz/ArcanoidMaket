using Dataers;
using DG.Tweening;
using Libs;
using Models.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.Popups
{
    public class GameWinPopup : Popup
    {
        private Foreground _foreground;
        
        [SerializeField] 
        private TextMeshProUGUI titleText;
        [SerializeField] 
        private Image progress;
        [SerializeField] 
        private float animationDuration = 1.0f;

        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        private void Start()
        {
            titleText.text = LocaleManager.GetText("gameWinPopupTitle");

            var info = LevelLoader.GetLevelInfo();
            progress.fillAmount = info.x / info.y;
        }

        public override void Init(Foreground foreground)
        {
            _foreground = foreground;
        }

        public override void Show()
        {
            transform.DOScale(Vector3.one, animationDuration);
        }

        public override void Hide()
        {
            transform.DOScale(Vector3.zero, animationDuration);
        }

        public void OnNextPush()
        {
            if (PlayerEnergy.Energy < 1) return;

            LevelManager.LoadNextLevel();
            Hide();
        }

        public void OnExitPush()
        {
            Hide();
            _foreground.Show(animationDuration);
            StartCoroutine(SceneChanger.WaitAndChange("Levels", animationDuration));
        }
    }
}