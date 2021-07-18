using System;
using Controllers.Managers;
using Controllers.Pools;
using ScriptObjects;
using UnityEngine;

namespace Logics
{
    public class Ball : MonoBehaviour, IPoolable
    {
        private GameConfig _config;
        private SpawnManager _spawnManager;
        private float _speedUpTimer;
        private float _speedDownTimer;
        private float _speed;

        [SerializeField]
        private Rigidbody2D ballRigidbody;
        
        public event Action<Ball, Collision2D> OnBallCollision;
        public int id;
        
        public void Init(Vector2 platformPosition, GameConfig config, SpawnManager spawnManager)
        {
            _config = config;
            _spawnManager = spawnManager;
            _speed = 1;
            _speedUpTimer = 0;
            _speedDownTimer = 0;
            
            var position = new Vector2(0, _config.startBallHeight);
            
            ballRigidbody.position = position;
            transform.position = position;
            
            ballRigidbody.AddForce((platformPosition - position).normalized * _config.ballStartForce);

            EventsAndStates.OnGameWin += Remove;
            EventsAndStates.OnGameOver += Remove;
            BonusManager.OnBulletBonus += BulletBonus;
        }

        private void BulletBonus(BlockTypes blockType)
        {
            switch (blockType)
            {
                case BlockTypes.BallSpeedUp:
                    _speedUpTimer = 5;
                    break;
                case BlockTypes.BallSpeedDown:
                    _speedDownTimer = 5;
                    break;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            ballRigidbody.velocity = AngleChecker(ballRigidbody.velocity).normalized * _speed * _config.ballVelocity;
            
            OnBallCollision?.Invoke(this, other);
        }

        private void Update()
        {
            ballRigidbody.simulated = EventsAndStates.IsGameRun;

            if (_speedUpTimer > 0)
            {
                _speed = 1.5f;
                _speedUpTimer -= Time.deltaTime;
            }
            else if (_speedDownTimer > 0)
            {
                _speed = 0.5f;
                _speedDownTimer -= Time.deltaTime;
            }
            else
            {
                _speed = 1;
            }
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
            EventsAndStates.OnGameWin -= Remove;
            EventsAndStates.OnGameOver -= Remove;
            BonusManager.OnBulletBonus -= BulletBonus;
            
            _spawnManager.Remove(this);
        }

        public void Activate()
        {
            OnBallCollision = null;
            
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
