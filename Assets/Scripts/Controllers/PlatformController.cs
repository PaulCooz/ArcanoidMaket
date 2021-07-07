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
        
        private void Update()
        {
            if (!Input.GetMouseButton(0)) return;
            
            platform.MoveTo(mainCamera.ScreenToWorldPoint(Input.mousePosition).x);
        }
    }
}
