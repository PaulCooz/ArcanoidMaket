using Libs;
using ScriptObjects;
using UnityEngine;

namespace View
{
    public class BallSpirit : MonoBehaviour
    {
        [SerializeField] 
        private GameConfig config;
        [SerializeField] 
        private Camera mainCamera;
        [SerializeField] 
        private Rigidbody2D platform;

        public void Show()
        {
            gameObject.SetActive(true);

            var position = platform.position;
            position.y = config.startBallHeight;
            
            transform.position = position;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
