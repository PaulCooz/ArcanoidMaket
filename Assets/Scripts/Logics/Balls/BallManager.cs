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

        public int countBalls;

        private void Start()
        {
            countBalls = 0;
        }

        private void Update()
        {
            if (!EventsAndStates.IsGameRun || countBalls != 0) return;
            
            NewBall();
        }

        private void RemoveBall()
        {
            countBalls--;
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

            countBalls++;
        }
    }
}
