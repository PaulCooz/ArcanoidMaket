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

            ballSpirit.Show(new Vector3(0, config.startBallHeight, 0));
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
            var ball = spawnManager.GetBall();
            
            ball.transform.SetParent(transform);
            ball.Init(new Vector2(0, config.startBallHeight), platformRigidbody.position, config, spawnManager);
            
            ball.OnBallCollision += bottom.BallTouched;
            ball.OnBallCollision += blockManager.SomeBlockTouched;
            ball.id = _balls.Count;

            _balls.Add(ball);
            _countBalls++;
        }
        
        public void NewBall(Block block)
        {
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
                ball.ChangeSpeed(coefficient, 5);
            }
        }
        
        public void ChangeDamage(int coefficient)
        {
            foreach (var ball in _balls)
            {
                ball.ChangeDamage(coefficient, 5);
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