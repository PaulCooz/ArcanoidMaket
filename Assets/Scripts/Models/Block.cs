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

        [SerializeField] 
        private Collider2D blockCollider;
        
        public BlockView blockView;
        public BlockType type;
        public int id;
        public bool isHittable;

        public void Init(SpawnManager spawnManager, int hitPoint, UnityEvent<Block> endAction)
        {
            _spawnManager = spawnManager;
            _hitPoint = hitPoint;
            _endAction = endAction;
        }

        public void Touch(int damage)
        {
            if (!isHittable) return;

            _hitPoint -= damage;
            blockView.Touch();

            if (_hitPoint <= 0) Remove();
        }
        
        public void Remove()
        {
            blockView.Pop();
            
            _spawnManager.Remove(this);

            if (EventsAndStates.IsGameRun)
            {
                _endAction?.Invoke(this);
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