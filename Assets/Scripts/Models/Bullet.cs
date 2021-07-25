using Models.Managers;
using Models.Pools;
using UnityEngine;
using UnityEngine.Events;

namespace Models
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        private SpawnManager _spawnManager;
        private BlockType _blockType;
        private UnityEvent<BlockType> _endAction;

        [SerializeField] 
        private SpriteRenderer bulletRenderer;

        public void Init(SpawnManager spawnManager, BlockType blockType, UnityEvent<BlockType> endAction, Color color)
        {
            _spawnManager = spawnManager;
            _blockType = blockType;
            _endAction = endAction;
            bulletRenderer.color = color;
            
            EventsAndStates.OnGameWin += Remove;
            EventsAndStates.OnGameOver += Remove;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("platform"))
            {
                _endAction?.Invoke(_blockType);
                Remove();
            }
            else if (other.gameObject.CompareTag("bottom"))
            {
                Remove();
            }
        }
        
        private void Remove()
        {
            EventsAndStates.OnGameWin -= Remove;
            EventsAndStates.OnGameOver -= Remove;
            
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
