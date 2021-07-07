using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    private Vector3 _lastScale;
    
    [SerializeField] 
    private float duration = 1.0f;
    [SerializeField]
    private GameObject popup;

    public void Show()
    {
        popup.transform.DOScale(_lastScale, duration);
    }

    public void Hide()
    {
        _lastScale = popup.transform.localScale;
        popup.transform.DOScale(Vector3.zero, duration);
    }
}
