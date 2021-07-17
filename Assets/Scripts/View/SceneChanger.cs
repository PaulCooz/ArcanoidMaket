using DG.Tweening;
using Logics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    public class SceneChanger : MonoBehaviour
    {
        private void Awake()
        {
            EventsAndStates.OnPackDone += LoadLevels;
        }

        private static void LoadLevels()
        {
            LoadScene("levels");
        }

        public static void LoadScene(string sceneName)
        {
            EventsAndStates.IsGameRun = false;
            SceneManager.LoadScene(sceneName);
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
            EventsAndStates.OnPackDone -= LoadLevels;
        }
    }
}
