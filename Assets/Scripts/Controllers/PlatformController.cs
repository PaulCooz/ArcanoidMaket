using Libs;
using Logics;
using ScriptObjects;
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
        private GameConfig config;
        
        private void Update()
        {
            if (!Input.GetMouseButton(0) || 
                Input.mousePosition.y >= config.uiZone * Screen.height || 
                !EventsAndStates.IsGameRun)
            {
                return;
            }
            
            platform.MoveTo(mainCamera.ScreenToWorldPoint(Input.mousePosition).x);
        }
    }
}
