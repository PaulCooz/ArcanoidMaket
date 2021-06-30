using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts.Models
{
    public class BorderManager : MonoBehaviour
    {
        public EdgeCollider2D borderPoints;
    
        private void Start()
        {
            var screenSize = Camera.main.ScreenToWorldPoint(Vector3.one);
            var newPoints = new List<Vector2>
            {
                new Vector2(-screenSize.x, screenSize.y),
                new Vector2(screenSize.x, screenSize.y),
                new Vector2(screenSize.x, -screenSize.y),
                new Vector2(-screenSize.x, -screenSize.y),
                new Vector2(-screenSize.x, screenSize.y)
            };

            borderPoints.SetPoints(newPoints);
        }
    }
}
