using UnityEngine;

namespace Logics
{
    public class Bottom : MonoBehaviour
    {
        public void BallTouched(Balls.Ball ball, Collision2D collision)
        {
            if (collision.gameObject == gameObject)
            {
                ball.Remove();
            }
        }
    }
}
