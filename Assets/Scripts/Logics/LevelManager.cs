using Dataers;
using UnityEngine;

namespace Logics
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelData _levelData;

        private void Awake()
        {
            EventsAndStates.OnGameWin += PlayerData.IncLastLevel;
            EventsAndStates.OnPackDone += PlayerData.IncLastPack;
        }

        private void Start()
        {
            LevelLoader.SetLevel(PlayerData.GetLastPack() == DataHolder.PackNumber ? PlayerData.GetLastLevel() : 0);

            LoadNextLevel();
        }

        public static void LoadNextLevel()
        {
            var nextData = LevelLoader.GetNextLevel();
            if (nextData == null) return;

            _levelData = (LevelData) nextData;

            EventsAndStates.SetGameStart(_levelData);
        }

        public static void RestartLevel()
        {
            EventsAndStates.SetGameStart(_levelData);
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameWin -= PlayerData.IncLastLevel;
            EventsAndStates.OnPackDone -= PlayerData.IncLastPack;
        }
    }
}