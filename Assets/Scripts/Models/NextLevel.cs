using UnityEngine;

namespace Models
{
    public class NextLevel : MonoBehaviour
    {
        [SerializeField]
        private GridOfBlocks gridOfBlocks;
        [SerializeField]
        private LevelManager levelManager;
    
        void Start()
        {
            LoadNextLevel();
        }

        private void LoadNextLevel()
        {
            gridOfBlocks.SetNewGrid(levelManager.GetNextLevel());
        }
    }
}
