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
        //                  1,  2,  3,  4,
        //                  5,  6,  7,  8, 
        //                  9, 10, 11, 12,
        //                 13, 14, 15, 16
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
    
        public static void SetLevelPack(TextAsset[] packLevels, int packNumber, Image image)
        {
            Levels = packLevels;
            PackNumber = packNumber;
            PackImage = image;
        } 
    }
}
