using System;
using Libs.Interfaces;
using Models.Managers;
using ScriptObjects;
using UnityEngine;

namespace Models
{
    public class Ball : MonoBehaviour, IPoolable
    {
        private GameConfig _config;
        private Camera _mainCamera;
        private SpawnManager _spawnManager;
        private float _ballForce;
        
        [SerializeField]
        private Rigidbody2D ballRigidbody;

        public Action OnDeactivate;

        public void Init(Vector2 platformPosition, GameConfig config, Camera mainCamera, SpawnManager spawnManager)
        {
            _config = config;
            _mainCamera = mainCamera;
            _spawnManager = spawnManager;

            ballRigidbody.position = new Vector3(0, -3, 0);

            Push(platformPosition, _config.pushBallForce);
        }

        private void Push(Vector2 toPosition, float ballForce)
        {
            _ballForce = ballForce;
            
            var position = transform.position;
            var impulse = toPosition - new Vector2(position.x, position.y);
            
            ballRigidbody.velocity = Vector2.zero;
            ballRigidbody.AddForce(impulse * _ballForce);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("bottom"))
            {
                _spawnManager.Remove(this);
            }
            else if (other.gameObject.CompareTag("platform"))
            {
                ballRigidbody.velocity = Vector2.zero;
                ballRigidbody.AddForce((transform.position - other.transform.position) * _ballForce);
            }
        }

        public void Activate()
        {
            OnDeactivate = null;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            ballRigidbody.velocity = Vector2.zero;
            OnDeactivate?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
