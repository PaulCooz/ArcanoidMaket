using DG.Tweening;
using UnityEngine;

namespace Resources.Scripts.Controls
{
    public class PlatformMover : MonoBehaviour
    {
        private const float MovingDuration = 0.1f;
        private const float MinX = -1.8f;
        private const float MaxX = 1.8f;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var move = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
                var currentPosition = transform.position;
                move.y = currentPosition.y;
                move.z = currentPosition.z;

                if (move.x < MinX) move.x = MinX;
                if (move.x > MaxX) move.x = MaxX;

                transform.DOMove(move, MovingDuration);
            }
        }
    }
}
