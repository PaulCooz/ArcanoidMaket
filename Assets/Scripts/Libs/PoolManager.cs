using System.Collections.Generic;
using UnityEngine;

namespace Libs
{
    public class PoolManager<T> : MonoBehaviour where T : MonoBehaviour, IPoolable
    {
        private readonly Dictionary<string, Queue<T>> _poolDictionary = new Dictionary<string, Queue<T>>();
        
        [SerializeField]
        private List<T> poolObjects;

        public T GetFromPool(string objectTag)
        {
            if (!_poolDictionary.ContainsKey(objectTag))
            {
                AddEmpty(objectTag);
            }
            if (_poolDictionary[objectTag].Count == 0)
            {
                AddWithSame(objectTag);
            }

            var obj = _poolDictionary[tag].Dequeue();
            obj.Activate();

            return obj;
        }

        public void SetToPool(T objectToReturn)
        {
            objectToReturn.Deactivate();
            _poolDictionary[objectToReturn.tag].Enqueue(objectToReturn);
        }

        private void AddWithSame(string objectTag)
        {
            foreach (var v in poolObjects)
            {
                if (v.CompareTag(objectTag))
                {
                    _poolDictionary[objectTag].Enqueue(Instantiate(v));
                    return;
                }
            }
        }

        private void AddEmpty(string objectTag)
        {
            _poolDictionary.Add(objectTag, new Queue<T>());
        }
    }
}
