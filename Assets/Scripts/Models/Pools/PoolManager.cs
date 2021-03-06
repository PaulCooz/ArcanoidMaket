using System.Collections.Generic;
using UnityEngine;

namespace Models.Pools
{
    public class PoolManager<T> : MonoBehaviour where T : MonoBehaviour, IPoolable
    {
        private Queue<T> _pool;
        
        [SerializeField]
        private T poolObject;

        private void Awake()
        {
            _pool = new Queue<T>();
        }

        public T GetFromPool()
        {
            if (_pool.Count == 0)
            {
                _pool.Enqueue(Instantiate(poolObject));
            }

            var obj = _pool.Dequeue();
            obj.Activate();

            return obj;
        }

        public void SetToPool(T objectToReturn)
        {
            objectToReturn.Deactivate();
            _pool.Enqueue(objectToReturn);
        }
    }
}
