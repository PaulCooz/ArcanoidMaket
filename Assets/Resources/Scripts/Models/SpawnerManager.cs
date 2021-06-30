using System.Collections.Generic;
using Resources.Scripts.Libs;
using UnityEngine;

namespace Resources.Scripts.Models
{
    public class SpawnerManager : MonoBehaviour
    {
        private Dictionary<int, Queue<Block>> _poolDictionary;
        
        public List<Pool> pools;
        
        private void Start()
        {
            _poolDictionary = new Dictionary<int, Queue<Block>>();

            foreach (var pool in pools)
            {
                var objectPool = new Queue<Block>();

                for(int i = 0; i < pool.poolSize; i++)
                {
                    var currentTransform = transform;
                    var newObject = Instantiate(pool.pref, currentTransform.position, Quaternion.identity, currentTransform);
                
                    newObject.gameObject.SetActive(false);
                    objectPool.Enqueue(newObject);
                }
                
                _poolDictionary.Add(pool.tag, objectPool);
            }
        }
    
        public Block SpawnFromPool(int poolTag)
        {
            if (!_poolDictionary.ContainsKey(poolTag))
            {
                Debug.LogWarning("missing pool " + poolTag);
                return null;
            }
            
            var objectToSpawn = _poolDictionary[poolTag].Dequeue();
            
            objectToSpawn.gameObject.SetActive(true);
            _poolDictionary[poolTag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }
}