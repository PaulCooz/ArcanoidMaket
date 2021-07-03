using Libs;
using UnityEngine;

namespace Models
{
    public class GridOfBlocks : MonoBehaviour
    {
        private int _width;
        private int _height;

        [SerializeField]
        private SpawnManager spawnManager;
        [SerializeField] [Range(0, 1)]
        private float height = 0.90f;
        [SerializeField] [Range(0, 1)]
        private float maxWidth = 1.00f;
        [SerializeField] [Range(0, 1)]
        private float maxHeight = 0.50f;
        [SerializeField] [Range(0, 1)]
        private float spaceByWidth = 0.01f;
        [SerializeField] [Range(0, 1)]
        private float spaceByHeight = 0.01f;
        [SerializeField]
        private Camera mainCamera;

        public void SetNewGrid(LevelData newData)
        {
            _width = newData.width;
            _height = newData.height;

            for (var i = 0; i < _width * _height; i++)
            {
                SetNewCell(newData.data[i], i);
            }

            transform.position = Transformer.Position(0.5f, height, mainCamera);
        }

        private void SetNewCell(string blockTag, int currentElement)
        {
            var newBlock = spawnManager.SpawnBlock(blockTag);
            newBlock.transform.SetParent(transform);

            var sizeX = (maxWidth - spaceByWidth * (_width + 1)) / _width;
            var sizeY = (maxHeight - spaceByHeight * (_height + 1)) / _height;

            var i = currentElement / _width;
            var j = currentElement % _width;

            var positionX = spaceByWidth * (j + 1) + j * sizeX + sizeX / 2;
            var positionY = spaceByHeight * (_height - i) + (_height - i - 1) * sizeY + sizeY / 2;
            
            newBlock.Init(positionX, positionY, sizeX, sizeY);
        }
    }
}
