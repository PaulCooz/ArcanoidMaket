using Logics;
using UnityEngine;

namespace View
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem particles;
        [SerializeField] 
        private Ball ball;

        public void Removing()
        {
            var newParticles = Instantiate(particles, ball.transform.position, Quaternion.identity);
            newParticles.transform.localScale = ball.transform.localScale;
        }
    }
}
