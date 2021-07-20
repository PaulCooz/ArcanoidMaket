using System;
using System.Collections;
using Models.Managers;
using Models.Pools;
using ScriptObjects;
using UnityEngine;
using View;

namespace Models
{
    public class Ball : MonoBehaviour, IPoolable
    {
        private GameConfig _config;
        private SpawnManager _spawnManager;
        private float _speed;

        [SerializeField]
        private Rigidbody2D ballRigidbody;
        [SerializeField] 
        private BallView ballView;

        public event Action<Ball, Collision2D> OnBallCollision;
        public int id;
        public int damage;
        
        public void Init(Vector2 ballPosition, Vector2 platformPosition, GameConfig config, SpawnManager spawnManager)
        {
            _config = config;
            _spawnManager = spawnManager;
            _speed = 1;
            damage = 1;

            ballRigidbody.position = ballPosition;
            transform.position = ballPosition;
            
            ballRigidbody.AddForce((platformPosition - ballPosition).normalized * _config.ballStartForce);

            EventsAndStates.OnGameWin += Remove;
            EventsAndStates.OnGameOver += Remove;
        }

        public void ChangeSpeed(float coefficient, float forTime)
        {
            print(coefficient + " " + forTime);
            StartCoroutine(SetSpeed(coefficient, forTime));
        }

        public void ChangeDamage(int coefficient, float forTime)
        {
            StartCoroutine(SetDamage(coefficient, forTime));
        }

        private IEnumerator SetSpeed(float coefficient, float forTime)
        {
            _speed *= coefficient;
            yield return new WaitForSeconds(forTime);
            _speed /= coefficient;
        }

        private IEnumerator SetDamage(int coefficient, float forTime)
        {
            damage *= coefficient;
            yield return new WaitForSeconds(forTime);
            damage /= coefficient;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            ballRigidbody.velocity = AngleChecker(ballRigidbody.velocity).normalized * _speed * _config.ballVelocity;

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
            EventsAndStates.OnGameWin -= Remove;
            EventsAndStates.OnGameOver -= Remove;
            
            ballView.Removing();
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
