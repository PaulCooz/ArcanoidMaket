using System;
using Libs;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Loaders
{
    [Serializable]
    public struct LevelData
    {
        public int height;
        public int width;
        public int[] data;
    }

    public class LevelLoader : MonoBehaviour
    {
        private int _currentLevel;

        private void Awake()
        {
            _currentLevel = 0;
        }

        public void SetLevel(int level)
        {
            _currentLevel = level;
        }

        public LevelData? GetNextLevel()
        {
            if (_currentLevel >= DataHolder.Levels.Length)
            {
                EventsAndStates.SetPackDone();
                return null;
            }

            var data = DataHolder.Levels[_currentLevel++].text;
            var jsonFile = JToken.Parse(data);

            var currentLevelData = new LevelData
            {
                height = (int) jsonFile["height"],
                width = (int) jsonFile["width"],
                data = jsonFile["layers"]?.First?["data"]?.ToObject<int[]>()
            };

            return currentLevelData;
        }

        public Vector2 GetLevelInfo()
        {
            return new Vector2(_currentLevel, DataHolder.Levels.Length);
        }
    }
}