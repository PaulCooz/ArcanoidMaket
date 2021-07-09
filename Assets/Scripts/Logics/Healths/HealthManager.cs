using Libs;
using Logics.Balls;
using Logics.Spawns;
using UnityEngine;

namespace Logics.Healths
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
        }

        public void SetHearts()
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
            for (var j = 0; j < startHeartsCount; j++)
            {
                _hearts[j] = spawnManager.GetHeart();
                _hearts[j].Init(grid[0, j].x, grid[0, j].y, grid[0, j].z, grid[0, j].w, spawnManager, mainCamera);
                _hearts[j].transform.SetParent(transform);
            }
        }

        public void PopHeart()
        {
            if (_activeHearts <= 0 || ballManager.countBalls > 0 || !EventsAndStates.IsGameRun) return;
            
            _activeHearts--;
            _hearts[_activeHearts].Pop();
            
            if (_activeHearts <= 0)
            {
                EventsAndStates.SetGameOver();
            }
        }
    }
}
