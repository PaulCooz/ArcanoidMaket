using System.Collections.Generic;
using Libs;
using UnityEngine;

namespace Logics
{
    public class Borders : MonoBehaviour
    {
        [SerializeField] 
        private Camera mainCamera;
        [SerializeField]
        private EdgeCollider2D bordersEdgeCollider;
        [SerializeField]
        private EdgeCollider2D bottomEdgeCollider;
        [SerializeField] [Range(0, 1)]
        private float bordersMaxHeight = 0.9f;
        [SerializeField] [Range(0, 1)]
        private float bottomAddLength = 0.2f;

        private void Start()
        {
            bordersEdgeCollider.SetPoints
            (
                new List<Vector2>
                {
                    Transformer.Position(0, 0, mainCamera),
                    Transformer.Position(0, bordersMaxHeight, mainCamera),
                    Transformer.Position(1, bordersMaxHeight, mainCamera),
                    Transformer.Position(1, 0, mainCamera)
                }
            );

            bottomEdgeCollider.SetPoints
            (
                new List<Vector2>
                {
                    Transformer.Position(-bottomAddLength, -0.1f, mainCamera),
                    Transformer.Position(1 + bottomAddLength, -0.1f, mainCamera)
                }
            );
        }
    }
}
