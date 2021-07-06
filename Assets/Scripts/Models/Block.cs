using System;
using Libs;
using Libs.Interfaces;
using Models.Managers;
using UnityEngine;

namespace Models
{
    public class Block : MonoBehaviour, IPoolable
    {
        private Camera _mainCamera;
        private SpawnManager _spawnManager;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public Action OnDeactivate;

        public void Init(float positionX, float positionY, float sizeX, float sizeY, SpawnManager spawnManager, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _spawnManager = spawnManager;
            
            transform.position = Transformer.Position(positionX, positionY, _mainCamera);
            transform.localScale = Transformer.Scale(sizeX, sizeY, _mainCamera, spriteRenderer);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("ball"))
            {
                _spawnManager.Remove(this);
            }
        }
        
        public void Activate()
        {
            OnDeactivate = null;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            OnDeactivate?.Invoke();
            gameObject.SetActive(false);
        }
    }
}