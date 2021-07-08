using System;
using Libs;
using UnityEngine;

namespace View
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] 
        private Popup gameOver;
        [SerializeField] 
        private Popup gameWin;

        public void Awake()
        {
            EventsAndStates.OnGameOver += ShowGameOver;
            EventsAndStates.OnGameWin += ShowGameWin;
        }

        private void ShowGameOver()
        {
            gameOver.gameObject.SetActive(true);
            gameOver.Show();
        }

        private void ShowGameWin()
        {
            gameWin.gameObject.SetActive(true);
            gameWin.Show();
        }

        public void OnDestroy()
        {
            EventsAndStates.OnGameOver -= ShowGameOver;
            EventsAndStates.OnGameWin -= ShowGameWin;
        }
    }
}
