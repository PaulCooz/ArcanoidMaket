using UnityEngine;

namespace Models.Managers
{
    public class BlockManager : MonoBehaviour
    {
        private int _height;
        private int _width;
        private Block[,] _grid;
        private int _emptyBlocks;
    
        [SerializeField]
        private GridOfBlocks gridOfBlocks;
        [SerializeField] 
        private LevelManager levelManager;

        public void NewLevel(LevelData levelData)
        {
            _height = levelData.height;
            _width = levelData.width;
            _grid = gridOfBlocks.NewGrid(levelData.height, levelData.width, levelData.data);

            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    _grid[i, j].OnDeactivate += PopBlock;
                }
            }
            _emptyBlocks = 0;
        }

        private void PopBlock()
        {
            _emptyBlocks++;
            if (_emptyBlocks == _width * _height)
            {
                levelManager.LoadNextLevel();
            }
        }
    }
}
