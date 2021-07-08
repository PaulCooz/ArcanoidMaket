using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace View
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] 
        private float duration = 1.0f;
        [SerializeField]
        private GameObject popup;

        public void Show()
        {
            popup.transform.DOScale(Vector3.one, duration);
        }

        public void Hide()
        {
            popup.transform.DOScale(Vector3.zero, duration);
            
            StartCoroutine(Disable(duration));
        }

        private IEnumerator Disable(float time)
        {
            yield return new WaitForSeconds(time);
            
            gameObject.SetActive(false);
        }
    }
}
