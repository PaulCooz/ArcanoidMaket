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

        public void PushRestart()
        {
            if (PlayerEnergy.Energy < 1) return;
            PlayerEnergy.DecEnergy();

            Hide();
            LevelManager.RestartLevel();
            Destroy(gameObject, 2 * animationDuration);
        }

        public void PushExit()
        {
            Hide();
            StartCoroutine(SceneChanger.WaitAndChange("Levels", 1));
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
