using System;

namespace Libs
{
    public static class EventsAndStates
    {
        public static bool IsGameRun = false;
        public static bool IsGameOver = true;
        public static bool IsWin = false;

        public static event Action OnGameStart;
        public static event Action OnGameWin;
        public static event Action OnGameOver;

        public static void SetGameWin()
        {
            IsWin = true;
            IsGameOver = false;
            IsGameRun = false;
            OnGameWin?.Invoke();
        }
        
        public static void SetGameStart()
        {
            IsWin = false;
            IsGameOver = false;
            IsGameRun = true;
            OnGameStart?.Invoke();
        }

        public static void SetGameOver()
        {
            IsGameOver = true;
            IsWin = false;
            IsGameRun = false;
            OnGameOver?.Invoke();
        }
    }
}
