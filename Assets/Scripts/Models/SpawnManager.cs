using UnityEngine;

namespace Models
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        private PoolBlockManager poolBlockManager;
        [SerializeField]
        private Camera mainCamera;
        
        public Block SpawnBlock(string blockTag)
        {
            var block = poolBlockManager.GetFromPool(blockTag);
            block.Init(mainCamera);

            return block;
        }
    }
}
