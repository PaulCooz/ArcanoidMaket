using System;
using Libs;
using ScriptObjects;
using UnityEngine;

namespace Models
{
    public class Ball : MonoBehaviour, IPoolable
    {
        private GameConfig _config;
        private Camera _mainCamera;
        
        [SerializeField]
        private Rigidbody2D ballRigidbody;

        public void Init(Vector2 platformPosition, GameConfig config, Camera mCamera)
        {
            _config = config;
            _mainCamera = mCamera;

            transform.position = new Vector3(0, -3, 0);

            Push(platformPosition, _config.pushBallForce);
        }

        private void Push(Vector2 toPosition, float ballForce)
        {
            var position = transform.position;
            var impulse = toPosition - new Vector2(position.x, position.y);
            
            ballRigidbody.AddForce(impulse * ballForce);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
