using ScriptObjects;
using UnityEngine;

namespace Controllers
{
    public class BallManagerController : MonoBehaviour
    {
        [SerializeField] 
        private GameConfig config;
        
        public bool MouseButtonUp()
        {
            return Input.GetMouseButtonUp(0) && Input.mousePosition.y < Screen.height * config.uiZone;
        }
    }
}