using System.Collections.Generic;
using Libs;
using Logics;
using Logics.Spawns;
using ScriptObjects;
using UnityEngine;
using View;

namespace Controllers.Managers
{
    public class BallManager : MonoBehaviour
    {
        private List<Ball> _balls;

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

            EventsAndStates.OnGameWin += ClearBalls;
            EventsAndStates.OnGameOver += ClearBalls;
        }

        private void Update()
        {
            if (!EventsAndStates.IsGameRun || CountBalls() != 0) return;

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
            _balls.RemoveAt(id);
            healthManager.PopHeart();
        }

        private void NewBall()
        {
            var ball = spawnManager.GetBall();
            
            ball.transform.SetParent(transform);
            ball.Init(platformRigidbody.position, config, spawnManager);
            
            ball.OnBallCollision += bottom.BallTouched;
            ball.OnBallCollision += blockManager.SomeBlockTouched;
            ball.id = _balls.Count;

            _balls.Add(ball);
        }

        private void ClearBalls()
        {
            _balls.Clear();
        }

        public int CountBalls()
        {
            return _balls.Count;
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameWin -= ClearBalls;
            EventsAndStates.OnGameOver -= ClearBalls;
        }
    }
}
