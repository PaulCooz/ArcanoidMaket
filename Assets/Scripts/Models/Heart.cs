using System;
using Libs;
using Libs.Interfaces;
using Models.Managers;
using UnityEngine;

namespace Models
{
    public class Heart : MonoBehaviour, IPoolable, IGridable
    {
        private Camera _mainCamera;
        private SpawnManager _spawnManager;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void Init(float positionX, float positionY, float sizeX, float sizeY, SpawnManager spawnManager, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _spawnManager = spawnManager;
            
            transform.position = Transformer.Position(positionX, positionY, _mainCamera);
            transform.localScale = Transformer.Scale(sizeX, sizeY, _mainCamera, spriteRenderer);
        }

        public void Pop()
        {
            _spawnManager.Remove(this);
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
