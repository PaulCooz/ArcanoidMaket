using System;
using Libs;
using Libs.Interfaces;
using Logics.Spawns;
using ScriptObjects;
using UnityEngine;

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
            ballRigidbody.AddForce((platformPosition - ballRigidbody.position) * _config.ballForce);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnBallCollision?.Invoke(this, other);
        }

        private void Update()
        {
            if (EventsAndStates.IsGameRun)
            {
                ballRigidbody.WakeUp();
            }
            else
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
