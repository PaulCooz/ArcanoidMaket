using UnityEngine;

namespace Libs
{
    public class DataHandler : MonoBehaviour
    {
        public void SetLevelPack(TextAsset[] packLevels, int packNumber)
        {
            DataHolder.Levels = packLevels;
            DataHolder.PackNumber = packNumber;
        }
    }
}
