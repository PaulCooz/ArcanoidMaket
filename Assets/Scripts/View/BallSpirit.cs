using UnityEngine;

namespace View
{
    public class BallSpirit : MonoBehaviour
    {
        [SerializeField] 
        private LineRenderer lineRenderer;
        [SerializeField] 
        private Rigidbody2D platform;

        public void Show(Vector3 position)
        {
            gameObject.SetActive(true);
            
            transform.position = position;
        
            lineRenderer.SetPositions
            (
                new Vector3[]
                {
                    position,
                    platform.position
                }
            );
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
