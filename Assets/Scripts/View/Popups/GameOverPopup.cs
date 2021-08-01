using Dataers;
using DG.Tweening;
using Libs;
using Models.Managers;
using TMPro;
using UnityEngine;

namespace View.Popups
{
    public class GameOverPopup : Popup
    {
        private PopupManager _popupManager;
        private Foreground _foreground;
        private bool _isHide;
        
        [SerializeField] 
        private TextMeshProUGUI titleText;
        [SerializeField] 
        private float animationsDuration = 1.0f;

        private void Awake()
        {
            transform.localScale = Vector3.zero;
            _isHide = false;
        }

        private void Start()
        {
            titleText.text = LocaleManager.GetText("gameOverPopupTitle");
        }

        public override void Init(PopupManager popupManager, Foreground foreground)
        {
            _popupManager = popupManager;
            _foreground = foreground;
        }

        public void PushRestart()
        {
            if (PlayerEnergy.Energy < 1)
            {
                _popupManager.ShowPopup<EnergyPopup>();
                return;
            }

            if (_isHide) return;
            _isHide = true;
            
            Hide();
            LevelManager.RestartLevel();
            Destroy(gameObject, animationsDuration + 0.1f);
        }

        public void PushExit()
        {
            if (_isHide) return;
            _isHide = true;
            
            Hide();
            
            _foreground.Show(animationsDuration);
            StartCoroutine(SceneChanger.WaitAndChange("Levels", animationsDuration));
        }

        public override void Show()
        {
            transform.DOScale(Vector3.one, animationsDuration);
        }

        public override void Hide()
        {
            transform.DOScale(Vector3.zero, animationsDuration);
        }
    }
}
