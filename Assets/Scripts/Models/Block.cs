using Models.Managers;
using Models.Pools;
using UnityEngine;
using UnityEngine.Events;
using View;

namespace Models
{
    public class Block : MonoBehaviour, IPoolable
    {
        private SpawnManager _spawnManager;
        private UnityEvent<Block> _endAction;
        private int _hitPoint;

        public BlockView blockView;
        public BlockType type;
        public int id;

        public void Init(SpawnManager spawnManager, int hitPoint, UnityEvent<Block> endAction)
        {
            _spawnManager = spawnManager;
            _hitPoint = hitPoint;
            _endAction = endAction;
        }
        
        public void Touch(int damage)
        {
            _hitPoint -= damage;

            if (_hitPoint <= 0) Remove();
        }
        
        public void Remove()
        {
            _spawnManager.Remove(this);
            _endAction?.Invoke(this);
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