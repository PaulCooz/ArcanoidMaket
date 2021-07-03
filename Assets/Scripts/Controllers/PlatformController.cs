using System;
using Libs;
using Models;
using ScriptObjects;
using UnityEngine;

namespace Controllers
{
    public class PlatformController : MonoBehaviour
    {
        private float _max;
        
        [SerializeField] 
        private Platform platform;
        [SerializeField]
        private Camera mainCamera;
        [SerializeField] 
        private float speed = 2.0f;
        [SerializeField] 
        private GameConfig config;

        private void Start()
        {
            _max = Math.Abs(Transformer.Position(config.platformWidth / 2.0f, 0, mainCamera).x);
        }

        private void Update()
        {
            if (!Input.GetMouseButton(0)) return;
            
            var position = platform.transform.position;
            var toPosition = new Vector3
            {
                x = Mathf.Clamp(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, -_max, _max),
                y = position.y,
                z = position.z
            };

            platform.transform.position = Vector3.Lerp(position, toPosition, Time.deltaTime * speed);
        }
    }
}
