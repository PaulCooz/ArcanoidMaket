using DG.Tweening;
using UnityEngine;

namespace Resources.Scripts.Controls
{
    public class PlatformMover : MonoBehaviour
    {
        private const float MovingDuration = 0.1f;
        private float _minX;
        private float _maxX;
    
        void Start()
        {
            var screenSize = Camera.main.ScreenToWorldPoint(Vector3.one) * 2.0f / 3.0f;

            _minX = screenSize.x;
            _maxX = -screenSize.x;
        }
    
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var move = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
                var currentPosition = transform.position;
                move.y = currentPosition.y;
                move.z = currentPosition.z;

                if (move.x < _minX) move.x = _minX;
                if (move.x > _maxX) move.x = _maxX;

                transform.DOMove(move, MovingDuration);
            }
        }
    }
}
