using Libs;
using UnityEngine;

namespace Models
{
    public class Block : MonoBehaviour, IPoolable
    {
        private Camera _mainCamera;
        
        [SerializeField] 
        private SpriteRenderer spriteRenderer;

        public void Init(Camera mCamera)
        {
            _mainCamera = mCamera;
        }
        
        public void Init(float positionX, float positionY, float sizeX, float sizeY)
        {
            transform.position = Transformer.Position(positionX, positionY, _mainCamera);
            transform.localScale = Transformer.Scale(sizeX, sizeY, _mainCamera, spriteRenderer);
        }
        
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}