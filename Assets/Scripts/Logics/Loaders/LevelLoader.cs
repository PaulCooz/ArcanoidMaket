using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Logics.Loaders
{
    [System.Serializable]
    public struct LevelData
    {
        public int height;
        public int width;
        public int[] data;
    }

    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        private TextAsset level;
    
        public LevelData GetNextLevel()
        {
            var jsonFile = JToken.Parse(level.text);

            var currentLevel = new LevelData
            {
                height = (int) jsonFile["height"],
                width = (int) jsonFile["width"],
                data = jsonFile["layers"]?.First?["data"]?.ToObject<int[]>()
            };
            
            return currentLevel;
        }
    }
}