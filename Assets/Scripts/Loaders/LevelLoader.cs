using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Loaders
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
        private int _currentLevel;

        private void Start()
        {
            _currentLevel = 0;
        }

        public LevelData GetNextLevel()
        {
            var path = "Packs/Pack" + DataHolder.LevelPack + "/level" + _currentLevel;
            var data = Resources.Load<TextAsset>(path)?.text;

            if (data == null)
            {
                DataHolder.LevelPack++;

                if (_currentLevel == 0)
                {
                    print("done");
                    return new LevelData();
                }
                else
                {
                    _currentLevel = 0;
                    return GetNextLevel();
                }
            }
            
            var jsonFile = JToken.Parse(data);

            var currentLevelData = new LevelData
            {
                height = (int) jsonFile["height"],
                width = (int) jsonFile["width"],
                data = jsonFile["layers"]?.First?["data"]?.ToObject<int[]>()
            };

            _currentLevel++;
            
            return currentLevelData;
        }
    }
}