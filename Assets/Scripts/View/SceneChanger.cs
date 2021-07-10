using DG.Tweening;
using Libs;
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

        private void LoadLevels()
        {
            LoadScene("levels");
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        private void OnDestroy()
        {
            EventsAndStates.OnPackDone -= LoadLevels;
            DOTween.KillAll();
        }
    }
}
