using UnityEngine;

namespace Models
{
    public class Transformer : MonoBehaviour
    {
        private Camera _mainCamera;
        
        [SerializeField]
        private Transform objectTransform;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void Init(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }
        
        public void SetPosition(float x, float y)
        {
            var cameraPosition = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width * x, Screen.height * y));
            cameraPosition.z = 0;
            
            objectTransform.position = cameraPosition;
        }

        public void SetSize(float x, float y)
        {
            if (spriteRenderer == null)
            {
                Debug.LogWarning("can't set sprite size (sprite render = null)");
                return;
            }
            
            var size = spriteRenderer.bounds.size * spriteRenderer.sprite.pixelsPerUnit;
            
            objectTransform.localScale = 2 * new Vector3(Screen.width * x / size.x, Screen.height * y / size.y, 1);
        }
    }
}
