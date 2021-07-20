using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Foreground : MonoBehaviour
    {
        [SerializeField] 
        private Image image;
        [SerializeField] 
        private float startHideDuration = 1;
        
        private void Awake()
        {
            image.color = Color.black;
            Hide(startHideDuration);
        }

        public void Show(float duration)
        {
            image.DOFade(1, duration);
        }

        public void Hide(float duration)
        {
            image.DOFade(0, duration);
        }
    }
}
