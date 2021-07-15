using System.IO;
using Libs;
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
            if ((int) jsonFile["lastPack"] != DataHolder.PackNumber) return;

            jsonFile["lastLevel"] = (int) jsonFile["lastLevel"] + 1;
            
            SaveData(jsonFile);
        }
        
        public static void IncLastPack()
        {
            var jsonFile = JToken.Parse(GetFileData());
            if ((int) jsonFile["lastPack"] != DataHolder.PackNumber) return;

            jsonFile["lastPack"] = (int) jsonFile["lastPack"] + 1;
            jsonFile["lastLevel"] = 0;
            
            SaveData(jsonFile);
        }

        private static void SaveData(JToken jToken)
        {
            var fs = new JsonTextWriter(File.CreateText(Application.dataPath + "/Resources/PlayerData.json"));
            
            jToken.WriteTo(fs);
            fs.Flush();
            fs.Close();
        }
    }
}