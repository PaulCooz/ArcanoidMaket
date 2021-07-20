using UnityEngine;
using UnityEngine.UI;

namespace Dataers
{
    public class DataHolder : MonoBehaviour
    {
        public static TextAsset[] Levels;
        //     = { new TextAsset(
        //     @"{
        //         ""height"":4,
        //         ""layers"":
        //         [{
        //             ""data"":
        //             [
        //                 0, 0, 0, 0,
        //                 2, 0, 0, 0,
        //                 1, 2, 0, 0,
        //                 2, 0, 0, 0
        //             ],
        //             ""height"":3,
        //             ""id"":1,
        //             ""width"":3
        //         }],
        //         ""width"":4
        //     }"
        // )};
        public static int PackNumber;
        // = 0;
        public static Image PackImage;

        // for debug {
        private void Awake()
        {
            PlayerPrefs.SetInt("lastPack", 100);
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
