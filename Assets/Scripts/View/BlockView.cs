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
        [SerializeField] 
        private ParticleSystem popParticles;
        
        public void Init(float positionX, float positionY, float sizeX, float sizeY, Sprite sprite, Camera mainCamera)
        {
            _mainCamera = mainCamera;

            spriteRenderer.sprite = sprite;
            transform.position = Transformer.Position(positionX, positionY, _mainCamera);
            transform.localScale = Transformer.Scale(sizeX, sizeY, _mainCamera, spriteRenderer);
        }

        public Color GetColor()
        {
            return spriteRenderer.color;
        }
        
        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
            
            var main = touchParticles.main;
            main.startColor = color;
        }

        public void Pop()
        {
            var particles = Instantiate(popParticles, transform.position, Quaternion.identity);
            var main = particles.main;
            
            var scale = transform.localScale;
            var min = Mathf.Min(scale.x, scale.y);
                
            particles.transform.localScale = new Vector3(min, min, 1);
            main.startColor = spriteRenderer.color;
        }

        public void Touch()
        {
            touchParticles.Play();
        }
    }
}
