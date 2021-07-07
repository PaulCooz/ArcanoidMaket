using System;
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

            ballRigidbody.position = new Vector3(0, -3, 0);

            Push(platformPosition);
        }

        private void Push(Vector2 toPosition)
        {
            var position = ballRigidbody.position;
            var impulse = toPosition - new Vector2(position.x, position.y);
            
            ballRigidbody.velocity = Vector2.zero;
            ballRigidbody.AddForce(impulse * _config.pushBallForce);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnBallCollision?.Invoke(this, other);
        }

        public void Remove()
        {
            _spawnManager.Remove(this);
        }

        public void Activate()
        {
            OnDeactivate = null;
            OnBallCollision = null;
            ballRigidbody.velocity = Vector2.zero;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            OnDeactivate?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
