using Newtonsoft.Json.Linq;
using Resources.Scripts.Libs;
using UnityEngine;

namespace Resources.Scripts.Models
{
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
                data = jsonFile["layers"]?.First?["data"]?.ToObject<int[]>()
            };

            return currentLevel;
        }
    }
}