using Dataers;
using Libs;
using Loaders;
using Logics;
using Logics.Healths;
using Logics.Loaders;
using Logics.Spawns;
using UnityEngine;

namespace Controllers.Managers
{
    public class HealthManager : MonoBehaviour
    {
        private const int Null = -1;
        
        private Heart[] _hearts;
        private int _activeHearts;

        [SerializeField]
        private int startHeartsCount;
        [SerializeField]
        private GridOfObjects gridOfObjects;
        [SerializeField]
        private SpawnManager spawnManager;
        [SerializeField] 
        private Camera mainCamera;
        [SerializeField] 
        private BallManager ballManager;

        private void Awake()
        {
            _activeHearts = Null;
            
            EventsAndStates.OnGameStart += SetHearts;
        }

        private void SetHearts(LevelData levelData)
        {
            if (_activeHearts != Null)
            {
                while (_activeHearts > 0)
                {
                    _hearts[--_activeHearts].Pop();
                }
            }

            MakeNewHeartLine();
        }

        private void MakeNewHeartLine()
        {
            _hearts = new Heart[startHeartsCount];
            _activeHearts = startHeartsCount;
            
            var grid = gridOfObjects.NewGrid(1, startHeartsCount);
            var cellSize = gridOfObjects.GetCellSize();
            
            for (var j = 0; j < startHeartsCount; j++)
            {
                _hearts[j] = spawnManager.GetHeart();
                _hearts[j].Init(grid[0, j].x, grid[0, j].y, cellSize.x, cellSize.y, spawnManager, mainCamera);
                _hearts[j].transform.SetParent(transform);
            }
        }

        public void PopHeart()
        {
            if (_activeHearts <= 0 || ballManager.CountBalls() > 0 || !EventsAndStates.IsGameRun) return;
            
            _activeHearts--;
            _hearts[_activeHearts].Pop();
            
            if (_activeHearts <= 0)
            {
                EventsAndStates.SetGameOver();
            }
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameStart -= SetHearts;
        }
    }
}
