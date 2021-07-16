using UnityEngine;
using UnityEngine.UI;

namespace Dataers
{
    public class DataHandler : MonoBehaviour
    {
        public void SetLevelPack(TextAsset[] packLevels, int packNumber, Image image)
        {
            DataHolder.Levels = packLevels;
            DataHolder.PackNumber = packNumber;
            DataHolder.PackImage = image;
        }
    }
}
