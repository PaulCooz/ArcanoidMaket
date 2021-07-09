using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    public class SceneChanger : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}
