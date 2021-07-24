using DG.Tweening;
using Libs;
using Models;
using Models.Managers;
using TMPro;
using UnityEngine;

namespace View.Popups
{
    public class PausePopup : Popup
    {
        private Foreground _foreground;
        
        [SerializeField] 
        private TextMeshProUGUI pauseTitle;
        [SerializeField]
        private float animationDuration;

        private void Start()
        {
            pauseTitle.text = LocaleManager.GetText("pauseTitle");
            EventsAndStates.IsGameRun = false;
        }

        public override void Init(Foreground foreground)
        {
            _foreground = foreground;
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
            Destroy(gameObject, animationDuration);
        }

        public void OnExitPush()
        {
            Hide();
            EventsAndStates.SetGameOver();
            
            _foreground.Show(animationDuration);
            StartCoroutine(SceneChanger.WaitAndChange("Levels", animationDuration));
        }

        private void OnDisable()
        {
            EventsAndStates.IsGameRun = true;
        }
    }
}
