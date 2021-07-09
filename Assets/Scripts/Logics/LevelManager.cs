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

        private void Start()
        {   
            LoadNextLevel();
        }

        public void LoadNextLevel()
        {
            _levelData = levelLoader.GetNextLevel();
            
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
    }
}