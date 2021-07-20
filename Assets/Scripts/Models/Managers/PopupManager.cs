using System.Collections.Generic;
using Libs;
using UnityEngine;

namespace Models.Managers
{
    public class PopupManager : MonoBehaviour
    {
        private static Stack<Popup> _currentPopups;
        
        [SerializeField]
        private Popup[] popups;

        private void Awake()
        {
            _currentPopups = new Stack<Popup>();
        }
        
        public void ShowPopup<T>() where T : Popup
        {
            foreach (var pool in popups)
            {
                if (pool.GetType() != typeof(T)) continue;
                
                var popup = Instantiate(pool, transform);

                popup.Show();
                _currentPopups.Push(popup);

                return;
            }
            
            Debug.LogWarning("can't find popup " + typeof(T));
        }

        public static void HidePopup()
        {
            if (_currentPopups.Count == 0)
            {
                Debug.LogWarning("can't hide popup: popup missed");
                return;
            }
            
            var popup = _currentPopups.Pop();
            popup.Hide();
        }
    }
}