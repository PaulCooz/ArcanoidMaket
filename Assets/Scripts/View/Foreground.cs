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
            image.raycastTarget = true;
            var tweener = image.DOFade(1, duration);
            tweener.onComplete += () => image.raycastTarget = false;
        }

        public void Hide(float duration)
        {
            image.raycastTarget = true;
            var tweener = image.DOFade(0, duration);
            tweener.onComplete += () => image.raycastTarget = false;
        }
    }
}
