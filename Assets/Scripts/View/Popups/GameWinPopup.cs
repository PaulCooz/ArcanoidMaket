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
        private bool _isHide;

        [SerializeField] 
        private TextMeshProUGUI titleText;
        [SerializeField] 
        private Image progress;
        [SerializeField] 
        private float animationsDuration = 1.0f;

        private void Awake()
        {
            transform.localScale = Vector3.zero;
            _isHide = false;
        }

        private void Start()
        {
            titleText.text = LocaleManager.GetText("gameWinPopupTitle");

            var info = LevelLoader.GetLevelInfo();
            progress.DOFillAmount(info.x / info.y, animationsDuration);
        }

        public override void Init(Foreground foreground)
        {
            _foreground = foreground;
        }

        public override void Show()
        {
            transform.DOScale(Vector3.one, animationsDuration);
        }

        public override void Hide()
        {
            transform.DOScale(Vector3.zero, animationsDuration);
        }

        public void OnNextPush()
        {
            if (_isHide) return;
            _isHide = true;
            
            if (PlayerEnergy.Energy < 1) return;

            LevelManager.LoadNextLevel();
            Hide();
            Destroy(gameObject, animationsDuration + 0.1f);
        }

        public void OnExitPush()
        {
            if (_isHide) return;
            _isHide = true;
            
            Hide();
            _foreground.Show(animationsDuration);
            StartCoroutine(SceneChanger.WaitAndChange("Levels", animationsDuration));
        }
    }
}