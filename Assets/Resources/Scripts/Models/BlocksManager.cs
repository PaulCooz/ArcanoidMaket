using UnityEngine;

namespace Resources.Scripts.Models
{
    public class BlocksManager : MonoBehaviour
    {
        public LevelManager levelManager;
        public BlocksGrid blocksGrid;

        private void Start()
        {
            NextLevel();
        }

        private void NextLevel()
        {
            var nextData = levelManager.GetNextLevel();
            blocksGrid.SetNewGrid(nextData);
        }
    }
}
