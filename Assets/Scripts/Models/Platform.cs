using System.Collections;
using DG.Tweening;
using Libs;
using Models.Managers;
using ScriptObjects;
using UnityEngine;

namespace Models
{
    public class Platform : MonoBehaviour
    {
        private float _max;
        private float _moveTimeCoefficient;
        
        [SerializeField]
        private Camera mainCamera;
        [SerializeField] 
        private SpriteRenderer spriteRenderer;
        [SerializeField] 
        private GameConfig config;
        [SerializeField] 
        private Rigidbody2D platformRigidbody;

        private void Start()
        {
            _moveTimeCoefficient = 1;
            
            SetSize(1, 1);
        }

        public void ChangeWidth(float coefficient)
        {
            StartCoroutine(SetPlatformWidth(coefficient, 5));
        }
        
        public void ChangeSpeed(float coefficient)
        {
            StartCoroutine(SetPlatformSpeed(coefficient, 5));
        }

        private IEnumerator SetPlatformWidth(float coefficient, float forTime)
        {
            SetSize(coefficient, 1);
            yield return new WaitForSeconds(forTime);
            SetSize(1, 1);
        }

        private IEnumerator SetPlatformSpeed(float coefficient, float forTime)
        {
            _moveTimeCoefficient /= coefficient;
            yield return new WaitForSeconds(forTime);
            _moveTimeCoefficient *= coefficient;
        }

        private void SetSize(float coefficientWidth, float coefficientHeight)
        {
            var width = coefficientWidth * config.platformWidth;
            var height = coefficientHeight * config.platformHeight;
            
            transform.localScale = Transformer.Scale(width, height, mainCamera, spriteRenderer); 
            _max = Mathf.Abs(Transformer.Position(width / 2.0f, 0, mainCamera).x);
        }

        public void MoveTo(float positionX)
        {
            platformRigidbody.DOKill();
            platformRigidbody.DOMoveX(Mathf.Clamp(positionX, -_max, _max), _moveTimeCoefficient * Time.deltaTime / config.platformSpeed);
        }
    }
}