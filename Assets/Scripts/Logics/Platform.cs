using Libs;
using ScriptObjects;
using UnityEngine;

namespace Logics
{
    public class Platform : MonoBehaviour
    {
        private float _max;
        
        [SerializeField]
        private Camera mainCamera;
        [SerializeField] 
        private SpriteRenderer spriteRenderer;
        [SerializeField] 
        private GameConfig config;
        [SerializeField] 
        private Rigidbody2D platformRigidbody;

        private void Start()
        {
            transform.localScale = Transformer.Scale(config.platformWidth, config.platformHeight, mainCamera, spriteRenderer);
            _max = Mathf.Abs(Transformer.Position(config.platformWidth / 2.0f, 0, mainCamera).x);
        }

        public void MoveTo(float positionX)
        {
            var position = platformRigidbody.position;
            var x = Mathf.Lerp(Mathf.Clamp(positionX, -_max, _max), position.x, Time.deltaTime);

            platformRigidbody.position = new Vector2(x, position.y);
        }
    }
}
