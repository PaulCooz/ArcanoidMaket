using Dataers;
using DG.Tweening;
using Libs;
using Models.Managers;
using UnityEngine;

namespace View.Popups
{
    public class EnergyPopup : Popup
    {
        private Foreground _foreground;
        private PopupManager _popupManager;
    
        [SerializeField]
        private float animationsDuration = 1.0f;
    
        public override void Init(PopupManager popupManager, Foreground foreground)
        {
            _popupManager = popupManager;
            _foreground = foreground;
        }

        public override void Show()
        {
            transform.DOScale(Vector3.one, animationsDuration);
        }

        public override void Hide()
        {
            transform.DOScale(Vector3.zero, animationsDuration);
            Destroy(gameObject, animationsDuration + 0.1f);
        }

        public void AddEnergy()
        {
            PlayerEnergy.IncEnergy();
        }
    }
}
