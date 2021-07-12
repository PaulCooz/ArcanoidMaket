using Libs;
using UnityEngine;

namespace View
{
    public class BlockView : MonoBehaviour
    {
        private Camera _mainCamera;
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField] 
        private ParticleSystem touchParticles;
        
        public void Init(float positionX, float positionY, float sizeX, float sizeY, Camera mainCamera)
        {
            _mainCamera = mainCamera;

            transform.position = Transformer.Position(positionX, positionY, _mainCamera);
            transform.localScale = Transformer.Scale(sizeX, sizeY, _mainCamera, spriteRenderer);
        }
        
        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public void Touch()
        {
            var particles = Instantiate(touchParticles, transform.position, Quaternion.identity);
            particles.startColor = spriteRenderer.color;
        }
    }
}
