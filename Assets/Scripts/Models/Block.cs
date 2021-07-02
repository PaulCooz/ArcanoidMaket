using Libs;
using UnityEngine;

namespace Models
{
    public class Block : MonoBehaviour, IPoolable
    {
        [SerializeField]
        private Transformer transformer;

        public void Init(float positionX, float positionY, float sizeX, float sizeY, Camera mainCamera)
        {
            transformer.Init(mainCamera);
            transformer.SetPosition(positionX, positionY);
            transformer.SetSize(sizeX, sizeY);
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