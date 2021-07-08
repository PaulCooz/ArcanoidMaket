using System;
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
        private Transform platformTransform;
        [SerializeField] 
        private HealthManager healthManager;
        [SerializeField] 
        private Bottom bottom;
        [SerializeField] 
        private BlockManager blockManager;

        private float _timerForTest;

        private void Start()
        {
            _timerForTest = 0;

            EventsAndStates.OnGameStart += NewBall;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && _timerForTest > 1.0f)
            {
                _timerForTest = 0;
                NewBall();
            }

            _timerForTest += Time.deltaTime;
        }

        private void NewBall()
        {
            var ball = spawnManager.GetBall();
            
            ball.transform.SetParent(transform);
            ball.Init(platformTransform.position, config, spawnManager);
            
            ball.OnBallCollision += bottom.BallTouched;
            ball.OnBallCollision += blockManager.SomeBlockTouched;
            ball.OnDeactivate += healthManager.PopHeart;
        }

        private void OnDestroy()
        {
            EventsAndStates.OnGameStart -= NewBall;
        }
    }
}
