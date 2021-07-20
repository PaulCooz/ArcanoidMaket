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
        //                  1,  1,  1,  1,
        //                  1,  1, 12, 11,
        //                  1, 12, 11,  1,
        //                 16, 16, 16, 16
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
