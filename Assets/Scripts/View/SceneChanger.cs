using System.Collections;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    public class SceneChanger : MonoBehaviour
    {
        public static IEnumerator WaitAndChange(string sceneName, float duration)
        {
            EventsAndStates.IsGameRun = false;
            yield return new WaitForSeconds(duration);
            SceneManager.LoadScene(sceneName);
        }

        public void SetMenuScene(float duration)
        {
            StartCoroutine(WaitAndChange("Menu", duration));
        }

        public void SetLevels(float duration)
        {
            StartCoroutine(WaitAndChange("Levels", duration));
        }
        
        public void SetGameScene(float duration)
        {
            StartCoroutine(WaitAndChange("Game", duration));
        }
    }
}
