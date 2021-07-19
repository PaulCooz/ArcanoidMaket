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
            if (!EventsAndStates.IsGameRun) return;
            
            popupManager.ShowPopup<GameWinPopup>();
        }
        
        public void ShowGameOver()
        {
            if (!EventsAndStates.IsGameRun) return;
            
            popupManager.ShowPopup<GameOverPopup>();
        }
        
        public void ShowGamePause()
        {
            if (!EventsAndStates.IsGameRun) return;
            
            popupManager.ShowPopup<PausePopup>();
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameWin -= ShowGameWin;
            EventsAndStates.OnGameOver -= ShowGameOver;
        }
    }
}
