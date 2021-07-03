using System.Collections.Generic;
using Libs;
using UnityEngine;

public class Borders : MonoBehaviour
{
    [SerializeField] 
    private Camera mainCamera;
    [SerializeField]
    private EdgeCollider2D edgeCollider;

    private void Start()
    {
        edgeCollider.SetPoints
        (
            new List<Vector2>
            {
                Transformer.Position(0, 0, mainCamera),
                Transformer.Position(0, 1, mainCamera),
                Transformer.Position(1, 1, mainCamera),
                Transformer.Position(1, 0, mainCamera)
            }
        );
    }
}
