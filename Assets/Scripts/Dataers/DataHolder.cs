using UnityEngine;
using UnityEngine.UI;

namespace Dataers
{
    public class DataHolder : MonoBehaviour
    {
        public static TextAsset[] Levels;
        public static int PackNumber;
        public static Image PackImage;
    
        public static void SetLevelPack(TextAsset[] packLevels, int packNumber, Image image)
        {
            Levels = packLevels;
            PackNumber = packNumber;
            PackImage = image;
        } 
    }
}
