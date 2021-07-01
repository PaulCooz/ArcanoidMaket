using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Models
{
    [System.Serializable]
    public struct LevelData
    {
        public int height;
        public int width;
        public string[] data;
    }

    public class LevelManager : MonoBehaviour
    {
        public TextAsset level;
    
        public LevelData GetNextLevel()
        {
            var jsonFile = JToken.Parse(level.text);

            var currentLevel = new LevelData
            {
                height = (int) jsonFile["height"],
                width = (int) jsonFile["width"],
                data = jsonFile["layers"]?.First?["data"]?.ToObject<string[]>()
            };
            
            return currentLevel;
        }
    }
}