using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class GridOfBlocks : MonoBehaviour
    {
        private List<Block> _blocksOnGrid;

        [SerializeField]
        private LevelManager levelManager;

        [SerializeField] 
        private SpawnManager spawnManager;

        public void SetNewLevel()
        {
            var newData = levelManager.GetNextLevel();
            var allBlocks = newData.height * newData.width;

            _blocksOnGrid = new List<Block>();
            for (int i = 0; i < allBlocks; i++)
            {
                SetNewCell(newData.data[i]);
            }
        }

        private void SetNewCell(string blockTag)
        {
            var newBlock = spawnManager.SpawnBlock(blockTag);
            newBlock.transform.SetParent(transform);
            
            _blocksOnGrid.Add(newBlock);
        }
    }
}
