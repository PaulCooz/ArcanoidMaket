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
        private Foreground _foreground;
        
        [SerializeField] 
        private TextMeshProUGUI titleText;
        [SerializeField] 
        private float animationDuration = 1.0f;

        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        private void Start()
        {
            titleText.text = LocaleManager.GetText("gameOverPopupTitle");
        }

        public override void Init(Foreground foreground)
        {
            _foreground = foreground;
        }

        public void PushRestart()
        {
            if (PlayerEnergy.Energy < 1) return;
            
            Hide();
            LevelManager.RestartLevel();
            Destroy(gameObject, animationDuration);
        }

        public void PushExit()
        {
            Hide();
            
            _foreground.Show(animationDuration);
            StartCoroutine(SceneChanger.WaitAndChange("Levels", animationDuration));
        }

        public override void Show()
        {
            transform.DOScale(Vector3.one, animationDuration);
        }

        public override void Hide()
        {
            transform.DOScale(Vector3.zero, animationDuration);
        }
    }
}
