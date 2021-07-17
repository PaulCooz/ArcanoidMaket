using Controllers.Managers;
using Logics;
using UnityEngine;
using View.Popups;

namespace Controllers
{
    public class PopupController : MonoBehaviour
    {
        [SerializeField] 
        private PopupManager popupManager;

        private void Awake()
        {
            EventsAndStates.OnGameWin += ShowGameWin;
            EventsAndStates.OnGameOver += ShowGameOver;
        }

        public void ShowGameWin()
        {
            popupManager.ShowPopup<GameWinPopup>();
        }
        
        public void ShowGameOver()
        {
            popupManager.ShowPopup<GameOverPopup>();
        }
        
        public void ShowGamePause()
        {
            popupManager.ShowPopup<PausePopup>();
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameWin -= ShowGameWin;
            EventsAndStates.OnGameOver -= ShowGameOver;
        }
    }
}
