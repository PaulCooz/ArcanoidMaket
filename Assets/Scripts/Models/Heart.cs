using System;
using DG.Tweening;
using Libs;
using Models.Managers;
using Models.Pools;
using UnityEngine;

namespace Models
{
    public class Heart : MonoBehaviour, IPoolable
    {
        private Camera _mainCamera;
        private SpawnManager _spawnManager;

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField] 
        private float animationsTime;

        public void Init(float positionX, float positionY, float sizeX, float sizeY, SpawnManager spawnManager, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _spawnManager = spawnManager;
            
            transform.position = Transformer.Position(positionX, positionY, _mainCamera);
            transform.DOScale(Transformer.Scale(sizeX, sizeY, _mainCamera, spriteRenderer), animationsTime);
        }

        public void Pop()
        {
            transform.DOScale(Vector3.zero, animationsTime).onComplete += () => _spawnManager.Remove(this);
        }

        public void Activate()
        {
            transform.localScale = Vector3.zero;
            
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
