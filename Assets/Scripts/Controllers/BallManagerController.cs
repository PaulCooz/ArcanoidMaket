using Models;
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
            return Input.mousePosition.y < Screen.height * config.uiZone && Input.GetMouseButtonUp(0);
        }
    }
}