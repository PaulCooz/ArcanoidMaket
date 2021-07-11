using System.Collections.Generic;
using Libs;
using Logics.Blocks;
using Logics.Healths;
using Logics.Spawns;
using ScriptObjects;
using UnityEngine;

namespace Logics.Balls
{
    public class BallManager : MonoBehaviour
    {
        private List<Ball> _balls; 
        
        [SerializeField]
        private GameConfig config;
        [SerializeField] 
        private SpawnManager spawnManager;
        [SerializeField] 
        private Rigidbody2D platformRigidbody;
        [SerializeField] 
        private HealthManager healthManager;
        [SerializeField] 
        private Bottom bottom;
        [SerializeField]
        private BlockManager blockManager;

        private void Start()
        {
            _balls = new List<Ball>();

            EventsAndStates.OnGameOver += ClearBalls;
            EventsAndStates.OnGameWin += ClearBalls;
        }

        private void Update()
        {
            if (!EventsAndStates.IsGameRun || CountBalls() != 0) return;
            
            NewBall();
        }

        private void RemoveBall()
        {
            _balls.RemoveAt(_balls.Count - 1);
        }

        private void NewBall()
        {
            var ball = spawnManager.GetBall();
            
            ball.transform.SetParent(transform);
            ball.Init(platformRigidbody.position, config, spawnManager);
            
            ball.OnBallCollision += bottom.BallTouched;
            ball.OnBallCollision += blockManager.SomeBlockTouched;
            ball.OnDeactivate += RemoveBall;
            ball.OnDeactivate += healthManager.PopHeart;
            
            _balls.Add(ball);
        }

        public int CountBalls()
        {
            return _balls.Count;
        }

        private void ClearBalls()
        {
            foreach (var ball in _balls)
            {
                ball.Remove();
            }
            _balls.Clear();
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameOver -= ClearBalls;
            EventsAndStates.OnGameWin -= ClearBalls;
        }
    }
}
