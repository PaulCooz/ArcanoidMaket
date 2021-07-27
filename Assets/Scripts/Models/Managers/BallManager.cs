using System.Collections.Generic;
using Controllers;
using ScriptObjects;
using UnityEngine;
using View;

namespace Models.Managers
{
    public class BallManager : MonoBehaviour
    {
        private List<Ball> _balls;
        private int _countBalls;

        [SerializeField] 
        private BallManagerController ballManagerController;
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
        [SerializeField] 
        private BallSpirit ballSpirit;

        private void Start()
        {
            _balls = new List<Ball>();
            _countBalls = 0;

            EventsAndStates.OnGameWin += ClearBalls;
            EventsAndStates.OnGameOver += ClearBalls;
        }

        private void Update()
        {
            if (!EventsAndStates.IsGameRun || _countBalls != 0) return;

            ballSpirit.Show();
            if (ballManagerController.MouseButtonUp())
            {
                NewBall();
                ballSpirit.Hide();
            }
        }

        public void RemoveBall(int id)
        {
            _balls[id].Remove();
            _countBalls--;
            
            healthManager.PopHeart(_countBalls);
        }

        private void NewBall()
        {
            if (!EventsAndStates.IsGameRun) return;
            
            var ball = spawnManager.GetBall();
            var position = platformRigidbody.position;
            
            ball.transform.SetParent(transform);
            ball.Init(new Vector2(position.x, config.startBallHeight), position, config, spawnManager);
            
            ball.OnBallCollision += bottom.BallTouched;
            ball.OnBallCollision += blockManager.SomeBlockTouched;
            ball.id = _balls.Count;

            _balls.Add(ball);
            _countBalls++;
        }
        
        public void NewBall(Block block)
        {
            if (!EventsAndStates.IsGameRun) return;

            var ball = spawnManager.GetBall();
            
            ball.transform.SetParent(transform);
            ball.Init(block.transform.position, platformRigidbody.position, config, spawnManager);
            
            ball.OnBallCollision += bottom.BallTouched;
            ball.OnBallCollision += blockManager.SomeBlockTouched;
            ball.id = _balls.Count;

            _balls.Add(ball);
            _countBalls++;
        }

        public void ChangeSpeed(float coefficient)
        {
            foreach (var ball in _balls)
            {
                if (!ball.isActiveAndEnabled) continue;
                
                ball.ChangeSpeed(coefficient, config.ballSpeedTime);
            }
        }

        public void SetFuryBalls()
        {
            foreach (var ball in _balls)
            {
                if (!ball.isActiveAndEnabled) continue;
                
                ball.SetFuryBall(config.ballFuryTime);
            }
        }
        
        private void ClearBalls()
        {
            _balls.Clear();
            _countBalls = 0;
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameWin -= ClearBalls;
            EventsAndStates.OnGameOver -= ClearBalls;
        }
    }
}
