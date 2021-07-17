using Controllers.Managers;
using DG.Tweening;
using Libs;
using Logics;
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
            Hide();
            LevelManager.RestartLevel();
            Destroy(gameObject, 2 * animationDuration);
        }

        public void PushExit()
        {
            Hide();
            SceneChanger.LoadScene("Levels");
            Destroy(gameObject, 2 * animationDuration);
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
