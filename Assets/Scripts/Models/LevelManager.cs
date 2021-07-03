using UnityEngine;

namespace Models
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GridOfBlocks gridOfBlocks;
        [SerializeField]
        private LevelLoader levelLoader;

        private void Start()
        {
            LoadNextLevel();
        }

        private void LoadNextLevel()
        {
            gridOfBlocks.SetNewGrid(levelLoader.GetNextLevel());
        }
    }
}