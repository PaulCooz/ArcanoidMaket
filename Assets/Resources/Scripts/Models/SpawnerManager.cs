using System.Collections.Generic;
using Resources.Scripts.Libs;
using UnityEngine;

namespace Resources.Scripts.Models
{
    public class SpawnerManager : MonoBehaviour
    {
        private Dictionary<int, Queue<GameObject>> _poolDictionary;
        
        public List<Pool> pools;
        
        private void Start()
        {
            _poolDictionary = new Dictionary<int, Queue<GameObject>>();

            foreach (var pool in pools)
            {
                var objectPool = new Queue<GameObject>();

                for(int i = 0; i < pool.poolSize; i++)
                {
                    var currentTransform = transform;
                    var newObject = Instantiate(pool.pref, currentTransform.position, Quaternion.identity, currentTransform);
                
                    newObject.SetActive(false);
                    objectPool.Enqueue(newObject);
                }
                
                _poolDictionary.Add(pool.tag, objectPool);
            }
        }
    
        public GameObject SpawnFromPool(int poolTag, Transform transformForSpawn)
        {
            if (!_poolDictionary.ContainsKey(poolTag))
            {
                Debug.LogWarning("missing pool " + poolTag);
                return null;
            }
            
            var objectToSpawn = _poolDictionary[poolTag].Dequeue();
            objectToSpawn.SetActive(true);

            objectToSpawn.transform.position = transformForSpawn.position;
            objectToSpawn.transform.rotation = transformForSpawn.rotation;

            _poolDictionary[poolTag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }
}