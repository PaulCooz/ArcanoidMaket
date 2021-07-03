using ScriptObjects;
using UnityEngine;

namespace Models
{
    public class BallManager : MonoBehaviour
    {
        [SerializeField] 
        private GameConfig config;
        [SerializeField] 
        private Camera mainCamera;
        [SerializeField] 
        private SpawnManager spawnManager;
        [SerializeField] 
        private Transform platformTransform;

        private float _timerForTest;
        
        private void Start()
        {
            _timerForTest = 0;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && _timerForTest > 2.0f)
            {
                _timerForTest = 0;
                
                var ball = spawnManager.SpawnBall("ball");
                ball.transform.SetParent(transform);
                ball.Init(platformTransform.position, config, mainCamera);
            }

            _timerForTest += Time.deltaTime;
        }
    }
}
