using System;
using System.Globalization;
using Models;
using ScriptObjects;
using UnityEngine;

namespace Dataers
{
    public class PlayerEnergy : MonoBehaviour
    {
        private const string LastEnergy = "lastEnergy";
        private const string LastDate = "lastData";

        [SerializeField]
        private GameConfig config;

        public static int Energy = -1;

        private void Awake()
        {
            if (Energy == -1)
            {
                EventsAndStates.OnGameStart += DecEnergy;
                EventsAndStates.OnGameWin += IncEnergy;
            }
            
            Energy = GetEnergy();
        }

        private int GetEnergy()
        {
            var lastEnergy = PlayerPrefs.GetInt(LastEnergy, config.maxEnergy);
            var lastDate = Convert.ToDateTime(PlayerPrefs.GetString(LastDate, DateTime.Now.ToString(CultureInfo.CurrentCulture)));

            return Mathf.Min(Convert.ToInt32(lastEnergy + (DateTime.Now - lastDate).TotalSeconds * config.energyPerSecond), config.maxEnergy);
        }

        private static void SaveEnergyData()
        {
            PlayerPrefs.SetInt(LastEnergy, Energy);
            PlayerPrefs.SetString(LastDate, DateTime.Now.ToString(CultureInfo.CurrentCulture));
        }

        private static void IncEnergy()
        {
            Energy++;

            SaveEnergyData();
        }

        private static void DecEnergy(LevelData levelData)
        {
            if (Energy < 1) return;
            Energy--;

            SaveEnergyData();
        }
    }
}