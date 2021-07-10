using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Loaders
{
    public static class PlayerData
    {
        private static string GetFileData()
        {
            
            return Resources.Load<TextAsset>("PlayerData").text;
        }

        public static int GetLastLevel()
        {
            return (int) JToken.Parse(GetFileData())["lastLevel"];
        }
        
        public static int GetLastPack()
        {
            return (int) JToken.Parse(GetFileData())["lastPack"];
        }

        public static void IncLastLevel()
        {
            var jsonFile = JToken.Parse(GetFileData());
            
            jsonFile["lastLevel"] = (int) jsonFile["lastLevel"] + 1;
            
            SaveData(jsonFile);
        }
        
        public static void IncLastPack()
        {
            var jsonFile = JToken.Parse(GetFileData());
            
            jsonFile["lastPack"] = (int) jsonFile["lastPack"] + 1;
            jsonFile["lastLevel"] = 0;
            
            SaveData(jsonFile);
        }

        private static void SaveData(JToken jToken)
        {
            var fs = new JsonTextWriter(File.CreateText("Assets/Resources/PlayerData.json"));
            
            jToken.WriteTo(fs);
            fs.Close();
        }
    }
}