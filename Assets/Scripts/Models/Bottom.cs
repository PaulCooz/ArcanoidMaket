using Models.Managers;
using UnityEngine;

namespace Models
{
    public class Bottom : MonoBehaviour
    {
        [SerializeField] 
        private BallManager ballManager;
        
        public void BallTouched(Ball ball, Collision2D collision)
        {
            if (collision.gameObject.CompareTag(gameObject.tag))
            {
                ballManager.RemoveBall(ball.id);
            }
        }
    }
}
