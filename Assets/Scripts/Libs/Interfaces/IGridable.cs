using Models.Managers;
using UnityEngine;

namespace Libs.Interfaces
{
    public interface IGridable
    {
        void Init(float positionX, float positionY, float sizeX, float sizeY, SpawnManager spawnManager, Camera mainCamera);
    }
}