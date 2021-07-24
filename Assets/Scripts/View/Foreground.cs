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
        private float hideDuration = 1;
        
        private void Awake()
        {
            image.color = Color.black;
            Hide(hideDuration);
        }

        public void Show()
        {
            image.raycastTarget = true;
            var tweener = image.DOFade(1, hideDuration);
            tweener.onComplete += () => image.raycastTarget = false;
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
