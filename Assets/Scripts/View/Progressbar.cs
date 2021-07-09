using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Progressbar : MonoBehaviour
    {
        [SerializeField]
        private Image bar;

        public void SetProgress(float x)
        {
            bar.fillAmount = x;
        }
    }
}
