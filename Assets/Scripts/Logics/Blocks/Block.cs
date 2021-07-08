using System;
using Libs;
using Libs.Interfaces;
using Logics.Spawns;
using UnityEngine;

namespace Logics.Blocks
{
    public class Block : MonoBehaviour, IPoolable
    {
        private Camera _mainCamera;
        private SpawnManager _spawnManager;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public int id;
        
        public event Action OnDeactivate;

        public void Init(float positionX, float positionY, float sizeX, float sizeY,
                         SpawnManager spawnManager, Camera mainCamera)
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

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public void Activate()
        {
            OnDeactivate = null;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            OnDeactivate?.Invoke();
        }
    }
}