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

        public void Init(SpawnManager spawnManager, BlockType blockType, UnityEvent<BlockType> endAction)
        {
            _spawnManager = spawnManager;
            _blockType = blockType;
            _endAction = endAction;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("platform"))
            {
                _endAction?.Invoke(_blockType);
                _spawnManager.Remove(this);
            }
            else if (other.gameObject.CompareTag("bottom"))
            {
                _spawnManager.Remove(this);
            }
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
