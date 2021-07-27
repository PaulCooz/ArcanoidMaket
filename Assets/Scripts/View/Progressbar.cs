using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Progressbar : MonoBehaviour
    {
        [SerializeField]
        private Image bar;
        [SerializeField]
        private float duration = 1;

        public void SetProgress(float x)
        {
            bar.DOFillAmount(x, duration);
        }
    }
}
