using System.Collections;
using Logics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    public class SceneChanger : MonoBehaviour
    {
        private const float ChangeTime = 1;

        public static IEnumerator WaitAndChange(string sceneName, float time)
        {
            EventsAndStates.IsGameRun = false;
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(sceneName);
        }

        public static IEnumerator LoadMenu()
        {
            return WaitAndChange("Menu", ChangeTime);
        }

        public static IEnumerator LoadLevels()
        {
            return WaitAndChange("Levels", ChangeTime);
        }

        public static IEnumerator LoadGame()
        {
            return WaitAndChange("Game", ChangeTime);
        }

        public void SetMenuScene()
        {
            StartCoroutine(LoadMenu());
        }

        public void SetLevels()
        {
            StartCoroutine(LoadLevels());
        }
        
        public void SetGameScene()
        {
            StartCoroutine(LoadGame());
        }
    }
}
