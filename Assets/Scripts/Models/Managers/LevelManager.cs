using Dataers;
using UnityEngine;
using View;

namespace Models.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelData _levelData;

        private void Awake()
        {
            EventsAndStates.OnGameWin += PlayerData.IncLastLevel;
            EventsAndStates.OnPackDone += PlayerData.IncLastPack;
            EventsAndStates.OnPackDone += SwapScene;
        }

        private void SwapScene()
        {
            StartCoroutine(SceneChanger.WaitAndChange("Levels", 1));
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
            EventsAndStates.OnPackDone -= SwapScene;
        }
    }
}
