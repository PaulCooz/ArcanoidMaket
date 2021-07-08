using Libs;
using Logics.Blocks;
using Logics.Healths;
using Logics.Loaders;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Logics
{
    public class LevelManager : MonoBehaviour
    {
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
            blockManager.NewLevel(levelLoader.GetNextLevel());
            SetHearts();
            
            EventsAndStates.SetGameStart();
        }

        private void SetHearts()
        {
            healthManager.SetHearts();
        }
    }
}