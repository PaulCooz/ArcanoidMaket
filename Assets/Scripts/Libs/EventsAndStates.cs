using System;

namespace Libs
{
    public static class EventsAndStates
    {
        public static bool IsGameOver;
        public static bool IsWin;

        public static event Action OnGameStart;
        public static event Action OnGameWin;
        public static event Action OnGameOver;

        public static void SetGameWin()
        {
            IsWin = true;
            IsGameOver = false;
            OnGameWin?.Invoke();
        }
        
        public static void SetGameStart()
        {
            IsWin = false;
            IsGameOver = false;
            OnGameStart?.Invoke();
        }

        public static void SetGameOver()
        {
            IsGameOver = true;
            IsWin = false;
            OnGameOver?.Invoke();
        }
    }
}
