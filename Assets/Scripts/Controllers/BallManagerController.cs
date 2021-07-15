using UnityEngine;

namespace Controllers
{
    public class BallManagerController : MonoBehaviour
    {
        public bool MouseButtonUp()
        {
            return Input.GetMouseButtonUp(0) && Input.mousePosition.y < Screen.height * 0.9f;
        }
    }
}