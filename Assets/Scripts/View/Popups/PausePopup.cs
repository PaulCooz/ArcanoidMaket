using DG.Tweening;
using Libs;
using Loaders;
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
            SceneChanger.LoadScene("Levels");
            Destroy(gameObject, 2 * animationDuration);
        }

        private void OnDisable()
        {
            EventsAndStates.IsGameRun = true;
        }
    }
}
