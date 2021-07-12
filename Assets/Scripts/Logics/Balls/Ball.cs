using System;
using Libs;
using Libs.Interfaces;
using Logics.Spawns;
using ScriptObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logics.Balls
{
    public class Ball : MonoBehaviour, IPoolable
    {
        private GameConfig _config;
        private SpawnManager _spawnManager;

        [SerializeField]
        private Rigidbody2D ballRigidbody;
        
        public event Action<Ball, Collision2D> OnBallCollision;
        public event Action OnDeactivate;

        public void Init(Vector2 platformPosition, GameConfig config, SpawnManager spawnManager)
        {
            _config = config;
            _spawnManager = spawnManager;

            ballRigidbody.position = new Vector3(platformPosition.x, _config.startBallHeight, 0);
            ballRigidbody.AddForce((platformPosition - ballRigidbody.position).normalized * _config.ballStartForce);
        }

        private static bool RandomBool(int chance)
        {
            return Random.Range(0, 100) < chance;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var angleCoeff = Random.Range(_config.minAngleCoeff, _config.maxAngleCoeff);
            var direction = RandomBool(50) ? -1f : 1f;
            var velocity = ballRigidbody.velocity;

            velocity = new Vector2(angleCoeff * velocity.x + direction * velocity.y, -direction * velocity.x + angleCoeff * velocity.y);
            velocity = velocity.normalized * _config.ballVelocity;
            
            ballRigidbody.velocity = velocity;

            OnBallCollision?.Invoke(this, other);
        }

        private void Update()
        {
            if (EventsAndStates.IsGameRun)
            {
                if (ballRigidbody.IsSleeping())
                {
                    ballRigidbody.WakeUp();
                    ballRigidbody.AddForce(Vector2.down * _config.ballStartForce);
                }
            }
            else if (!ballRigidbody.IsSleeping())
            {
                ballRigidbody.Sleep();
            }
        }

        public void Remove()
        {
            _spawnManager.Remove(this);
        }

        public void Activate()
        {
            OnDeactivate = null;
            OnBallCollision = null;
            
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
         
            if (EventsAndStates.IsGameRun) OnDeactivate?.Invoke();
        }
    }
}
