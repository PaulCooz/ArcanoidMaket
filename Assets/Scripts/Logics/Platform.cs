using System.Collections;
using Controllers.Managers;
using DG.Tweening;
using Libs;
using ScriptObjects;
using UnityEngine;

namespace Logics
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
            
            BonusManager.OnBulletBonus += BulletBonus;
        }

        private void BulletBonus(BlockTypes blockType)
        {
            switch (blockType)
            {
                case BlockTypes.PlatformDilator:
                    StartCoroutine(PlatformDilator());
                    break;
                
                case BlockTypes.PlatformNarrower:
                    StartCoroutine(PlatformNarrowed());
                    break;
                
                case BlockTypes.PlatformSpeedUp:
                    StartCoroutine(PlatformSpeedUp());
                    break;
                
                case BlockTypes.PlatformSpeedDown:
                    StartCoroutine(PlatformSpeedDown());
                    break;
            }
        }

        private IEnumerator PlatformDilator()
        {
            SetSize(1.5f, 1);
            yield return new WaitForSeconds(5);
            SetSize(1, 1);
        }
        
        private IEnumerator PlatformNarrowed()
        {
            SetSize(0.5f, 1);
            yield return new WaitForSeconds(5);
            SetSize(1, 1);
        }
        
        private IEnumerator PlatformSpeedUp()
        {
            _moveTimeCoefficient = 2;
            yield return new WaitForSeconds(5);
            _moveTimeCoefficient = 1;
        }
        
        private IEnumerator PlatformSpeedDown()
        {
            _moveTimeCoefficient = 0.5f;
            yield return new WaitForSeconds(5);
            _moveTimeCoefficient = 1;
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

        private void OnDestroy()
        {
            BonusManager.OnBulletBonus -= BulletBonus;
        }
    }
}
