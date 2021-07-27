using UnityEngine;
using UnityEngine.UI;

namespace Dataers
{
    public class DataHolder : MonoBehaviour
    {
        public static TextAsset[] Levels;
        public static int PackNumber;
        public static Image PackImage;

        // for debug {
        [SerializeField]
        public TextAsset[] testLevels;
        [SerializeField]
        public int testPackNumber;
        [SerializeField]
        public Image testPackImage;
        
        private void Awake()
        {
            Levels = testLevels;
            PackNumber = testPackNumber;
            PackImage = testPackImage;
            
            // PlayerPrefs.SetInt("lastPack", 100);
        }
        //}

        public static void SetLevelPack(TextAsset[] packLevels, int packNumber, Image image)
        {
            Levels = packLevels;
            PackNumber = packNumber;
            PackImage = image;
        } 
    }
}
