using System;
using Dataers;

namespace Models
{
    public static class EventsAndStates
    {
        public static bool IsGameRun = false;

        public static event Action<LevelData> OnGameStart;
        public static event Action OnGameWin;
        public static event Action OnGameOver;
        public static event Action OnPackDone;
        public static event Action OnChangeLocale;

        public static void SetGameWin()
        {
            OnGameWin?.Invoke();
            IsGameRun = false;
        }

        public static void SetGameStart(LevelData levelData)
        {
            OnGameStart?.Invoke(levelData);
            IsGameRun = true;
        }

        public static void SetGameOver()
        {
            OnGameOver?.Invoke();
            IsGameRun = false;
        }

        public static void SetPackDone()
        {
            OnPackDone?.Invoke();
            IsGameRun = false;
        }

        public static void ChangeLocale()
        {
            OnChangeLocale?.Invoke();
        }
    }
}
