using UnityEngine;

namespace Loaders
{
    public static class PlayerData
    {
        private const string LastLevel = "lastLevel";
        private const string LastPack = "lastPack";
        
        public static int GetLastLevel()
        {
            return PlayerPrefs.GetInt(LastLevel, 0);
        }
        
        public static int GetLastPack()
        {
            return PlayerPrefs.GetInt(LastPack, 0);
        }

        public static void IncLastLevel()
        {
            if (GetLastPack() != DataHolder.PackNumber) return;
            var lastLevel = GetLastLevel();

            PlayerPrefs.SetInt(LastLevel, ++lastLevel);
            PlayerPrefs.Save();
        }
        
        public static void IncLastPack()
        {
            var lastPack = GetLastPack();
            if (lastPack != DataHolder.PackNumber) return;

            PlayerPrefs.SetInt(LastPack, ++lastPack);
            PlayerPrefs.SetInt(LastLevel, 0);
            PlayerPrefs.Save();
        }
    }
}