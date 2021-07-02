using System;
using System.Collections.Generic;
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
        private float maxWidth = 1.00f;
        [SerializeField] [Range(0, 1)]
        private float maxHeight = 0.50f;
        [SerializeField] [Range(0, 1)]
        private float spaceByWidth = 0.01f;
        [SerializeField] [Range(0, 1)]
        private float spaceByHeight = 0.01f;
        [SerializeField]
        private Camera mainCamera;
        [SerializeField] 
        private Transformer transformer;

        private void Start()
        {
            transformer.Init(mainCamera);
            transformer.SetPosition(0.5f, 0.9f);
        }

        public void SetNewGrid(LevelData newData)
        {
            _width = newData.width;
            _height = newData.height;

            for (int i = 0; i < _width * _height; i++)
            {
                SetNewCell(newData.data[i], i);
            }
        }

        private void SetNewCell(string blockTag, int currentElement)
        {
            var newBlock = spawnManager.SpawnBlock(blockTag);
            newBlock.transform.SetParent(transform);

            float sizeX = (maxWidth - spaceByWidth * (_width + 1)) / _width;
            float sizeY = (maxHeight - spaceByHeight * (_height + 1)) / _height;

            int i = currentElement / _width;
            int j = currentElement % _width;

            float positionX = spaceByWidth * (j + 1) + j * sizeX + sizeX / 2;
            float positionY = spaceByHeight * (_height - i) + (_height - i - 1) * sizeY + sizeY / 2;
            
            newBlock.Init(positionX, positionY, sizeX, sizeY, mainCamera);
        }
    }
}
