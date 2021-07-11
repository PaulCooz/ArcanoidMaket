using Libs;
using Loaders;
using Logics.Blocks;
using Logics.Healths;
using UnityEngine;

namespace Logics
{
    public class LevelManager : MonoBehaviour
    {
        private LevelData _levelData;
        
        [SerializeField]
        private BlockManager blockManager;
        [SerializeField]
        private HealthManager healthManager;
        [SerializeField]
        private LevelLoader levelLoader;

        private void Awake()
        {
            EventsAndStates.OnGameWin += PlayerData.IncLastLevel;
            EventsAndStates.OnPackDone += PlayerData.IncLastPack;
        }

        private void Start()
        {
            if (PlayerData.GetLastPack() == DataHolder.PackNumber)
            {
                levelLoader.SetLevel(PlayerData.GetLastLevel());
            }
            
            LoadNextLevel();
        }

        public void LoadNextLevel()
        {
            var nextData = levelLoader.GetNextLevel();
            if (nextData == null) return;
            
            _levelData = (LevelData) nextData;

            blockManager.NewLevel(_levelData);
            SetHearts();
            
            EventsAndStates.SetGameStart();
        }

        public void RestartLevel()
        {
            blockManager.NewLevel(_levelData);
            SetHearts();
            
            EventsAndStates.SetGameStart();
        }

        private void SetHearts()
        {
            healthManager.SetHearts();
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameWin -= PlayerData.IncLastLevel;
            EventsAndStates.OnPackDone -= PlayerData.IncLastPack;
        }
    }
}