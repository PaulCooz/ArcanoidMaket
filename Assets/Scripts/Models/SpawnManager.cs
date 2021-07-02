using UnityEngine;

namespace Models
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        private PoolBlockManager poolBlockManager;
        
        public Block SpawnBlock(string blockTag)
        {
            return poolBlockManager.GetFromPool(blockTag);
        }
    }
}
