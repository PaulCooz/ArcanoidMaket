using Libs;
using Logics;
using UnityEngine;

namespace Controllers
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;
        [SerializeField] 
        private Platform platform;
        [SerializeField] 
        private float maxHeightInput = 0.9f;
        
        private void Update()
        {
            if (!Input.GetMouseButton(0) || 
                Input.mousePosition.y >= maxHeightInput * Screen.height || 
                !EventsAndStates.IsGameRun)
            {
                return;
            }
            
            platform.MoveTo(mainCamera.ScreenToWorldPoint(Input.mousePosition).x);
        }
    }
}
