using System.Collections.Generic;
using System.Linq;
using Dataers;
using Libs;
using UnityEngine;

namespace Models.Managers
{
    public class HealthManager : MonoBehaviour
    {
        private List<Heart> _hearts;
        private Vector2[] _positions;
        private Vector2 _cellSize;

        [SerializeField]
        private int startHeartsCount;
        [SerializeField] 
        private int maxHeartsCount;
        [SerializeField]
        private GridOfObjects gridOfObjects;
        [SerializeField]
        private SpawnManager spawnManager;
        [SerializeField] 
        private Camera mainCamera;

        private void Start()
        {
            MakeNewHeartLine();
            
            EventsAndStates.OnGameStart += SetHearts;
            EventsAndStates.OnGameWin += Clear;
            EventsAndStates.OnGameOver += Clear;
        }

        private void SetHearts(LevelData levelData)
        {
            Clear();
            MakeNewHeartLine();
        }

        private void Clear()
        {
            if (_hearts == null) return;
            
            while (_hearts.Count > 0)
            {
                _hearts.Last().Pop();
                _hearts.RemoveAt(_hearts.Count - 1);
            }
        }

        private void MakeNewHeartLine()
        {
            var grid = gridOfObjects.NewGrid(1, maxHeartsCount);
            _cellSize = gridOfObjects.GetCellSize();

            _positions = new Vector2[maxHeartsCount];
            for (var j = 0; j < maxHeartsCount; j++)
            {
                _positions[j] = grid[0, j];
            }

            _hearts = new List<Heart>();
            for (var j = 0; j < startHeartsCount; j++)
            {
                AddHeart();
            }
        }

        public void AddHeart()
        {
            if (_hearts.Count == maxHeartsCount) return;

            var heart = spawnManager.GetHeart();
            var position = _positions[_hearts.Count];
            
            heart.transform.SetParent(transform);
            heart.Init(position.x, position.y, _cellSize.x, _cellSize.y, spawnManager, mainCamera);
            
            _hearts.Add(heart);
        }

        public void PopHeart(int countBalls)
        {
            if (countBalls > 0 || !EventsAndStates.IsGameRun) return;
            if (_hearts.Count <= 0)
            {
                EventsAndStates.SetGameOver();
                return;
            }

            _hearts.Last().Pop();
            _hearts.RemoveAt(_hearts.Count - 1);
        }

        public void PopHeart()
        {
            if (!EventsAndStates.IsGameRun) return;
            if (_hearts.Count <= 0)
            {
                EventsAndStates.SetGameOver();
                return;
            }
            
            _hearts.Last().Pop();
            _hearts.RemoveAt(_hearts.Count - 1);
        }

        public int CountHearts()
        {
            return _hearts.Count;
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameStart -= SetHearts;
            EventsAndStates.OnGameWin -= Clear;
            EventsAndStates.OnGameOver -= Clear;
        }
    }
}
