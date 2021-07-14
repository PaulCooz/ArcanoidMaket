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

        public void Init(Vector2 platformPosition, float spawnPositionX, GameConfig config, SpawnManager spawnManager)
        {
            _config = config;
            _spawnManager = spawnManager;

            ballRigidbody.position = new Vector3(spawnPositionX, _config.startBallHeight, 0);
            ballRigidbody.AddForce((platformPosition - ballRigidbody.position).normalized * _config.ballStartForce);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            ballRigidbody.velocity = AngleChecker(ballRigidbody.velocity).normalized * _config.ballVelocity;
            
            OnBallCollision?.Invoke(this, other);
        }

        private void Update()
        {
            ballRigidbody.simulated = EventsAndStates.IsGameRun;
        }

        private Vector2 AngleChecker(Vector2 vector)
        {
            float? angle = null;
            
            if (vector.x >= 0 && vector.y >= 0)
            {
                if (Vector2.Angle(vector, Vector2.right) < _config.minAngle)
                {
                    angle = _config.minAngle;
                }
                if (Vector2.Angle(vector, Vector2.up) < _config.minAngle)
                {
                    angle = 90 - _config.minAngle;
                }
            }
            else if (vector.x < 0 && vector.y >= 0)
            {
                if (Vector2.Angle(vector, Vector2.left) < _config.minAngle)
                {
                    angle = 180 - _config.minAngle;
                }
                if (Vector2.Angle(vector, Vector2.up) < _config.minAngle)
                {
                    angle = 90 + _config.minAngle;
                }
            }
            else if (vector.x < 0 && vector.y < 0)
            {
                if (Vector2.Angle(vector, Vector2.left) < _config.minAngle)
                {
                    angle = 180 + _config.minAngle;
                }
                if (Vector2.Angle(vector, Vector2.down) < _config.minAngle)
                {
                    angle = 270 - _config.minAngle;
                }
            }
            else if (vector.x >= 0 && vector.y < 0)
            {
                if (Vector2.Angle(vector, Vector2.right) < _config.minAngle)
                {
                    angle = 360 - _config.minAngle;
                }
                if (Vector2.Angle(vector, Vector2.down) < _config.minAngle)
                {
                    angle = 270 + _config.minAngle;
                }
            }

            return angle != null ? new Vector2(Mathf.Cos((float) (angle * Mathf.Deg2Rad)), Mathf.Sin((float) (angle * Mathf.Deg2Rad))) : vector;
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
