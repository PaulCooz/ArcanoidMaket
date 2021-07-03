using Libs;
using ScriptObjects;
using UnityEngine;

namespace Models
{
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;
        [SerializeField] 
        private SpriteRenderer spriteRenderer;
        [SerializeField] 
        private GameConfig config;

        private void Start()
        {
            transform.localScale = Transformer.Scale(config.platformWidth, config.platformHeight, mainCamera, spriteRenderer);
        }
    }
}
