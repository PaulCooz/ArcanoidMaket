using System;
using Libs;
using Libs.Interfaces;
using Logics.Balls;
using Logics.Spawns;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Logics.Blocks
{
    public class Block : MonoBehaviour, IPoolable
    {
        private Camera _mainCamera;
        private SpawnManager _spawnManager;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public event Action OnDeactivate;

        public void Init(float positionX, float positionY, float sizeX, float sizeY, SpawnManager spawnManager, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _spawnManager = spawnManager;
            
            transform.position = Transformer.Position(positionX, positionY, _mainCamera);
            transform.localScale = Transformer.Scale(sizeX, sizeY, _mainCamera, spriteRenderer);
        }

        public void Remove()
        {
            _spawnManager.Remove(this);
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