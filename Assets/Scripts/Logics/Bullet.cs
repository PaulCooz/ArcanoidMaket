using Controllers.Managers;
using Controllers.Pools;
using UnityEngine;

namespace Logics
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        private SpawnManager _spawnManager;
        private BlockTypes _blockType;

        public void Init(SpawnManager spawnManager, BlockTypes blockType)
        {
            _spawnManager = spawnManager;
            _blockType = blockType;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("ball")) return;
            if (other.gameObject.CompareTag("platform"))
            {
                BonusManager.BulletBonus(_blockType);
            }
            
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
