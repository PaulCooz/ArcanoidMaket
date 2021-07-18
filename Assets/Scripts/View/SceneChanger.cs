using Logics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    public class SceneChanger : MonoBehaviour
    {
        public static void LoadScene(string sceneName)
        {
            EventsAndStates.IsGameRun = false;
            SceneManager.LoadScene(sceneName);
        }

        public static void LoadLevels()
        {
            LoadScene("levels");
        }
    }
}
