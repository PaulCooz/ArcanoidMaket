using System;
using System.Globalization;
using UnityEngine;

namespace Dataers
{
    public class PlayerEnergy : MonoBehaviour
    {
        private const string LastEnergy = "lastEnergy";
        private const string LastDate = "lastData";

        [SerializeField] 
        public int maxEnergy;
        [SerializeField] 
        public double energyPerSecond;

        public static int Energy;

        private void Awake()
        {
            Energy = GetEnergy();
        }

        private int GetEnergy()
        {
            var lastEnergy = PlayerPrefs.GetInt(LastEnergy, maxEnergy);
            var lastDate = Convert.ToDateTime(PlayerPrefs.GetString(LastDate, DateTime.Now.ToString(CultureInfo.CurrentCulture)));

            return Mathf.Min(Convert.ToInt32(lastEnergy + (DateTime.Now - lastDate).TotalSeconds * energyPerSecond), maxEnergy);
        }

        private static void SaveEnergyData()
        {
            PlayerPrefs.SetInt(LastEnergy, Energy);
            PlayerPrefs.SetString(LastDate, DateTime.Now.ToString(CultureInfo.CurrentCulture));
        }

        public static void DecEnergy()
        {
            if (Energy < 1) return;
            Energy--;

            SaveEnergyData();
        }
    }
}