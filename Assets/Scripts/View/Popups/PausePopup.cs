using Controllers.Managers;
using DG.Tweening;
using Libs;
using Logics;
using TMPro;
using UnityEngine;

namespace View.Popups
{
    public class PausePopup : Popup
    {
        [SerializeField] 
        private TextMeshProUGUI pauseTitle;
        [SerializeField]
        private float animationDuration;

        private void Start()
        {
            pauseTitle.text = LocaleManager.GetText("pauseTitle");
            
            EventsAndStates.IsGameRun = false;
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            transform.DOScale(Vector3.one, animationDuration);
        }

        public override void Hide()
        {
            transform.DOScale(Vector3.zero, animationDuration);
        }
        
        public void OnResumePush()
        {
            Hide();
            Destroy(gameObject, 2 * animationDuration);
        }

        public void OnExitPush()
        {
            Hide();
            EventsAndStates.SetGameOver();
            Destroy(gameObject, 2 * animationDuration);
            SceneChanger.LoadScene("Levels");
        }

        private void OnDisable()
        {
            EventsAndStates.IsGameRun = true;
        }
    }
}
