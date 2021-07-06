using ScriptObjects;
using UnityEngine;

namespace Models.Managers
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
        [SerializeField] 
        private HealthManager healthManager;

        private float _timerForTest;
        
        private void Start()
        {
            _timerForTest = 0;
            
            NewBall();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && _timerForTest > 1.0f)
            {
                _timerForTest = 0;
                
                NewBall();
            }

            _timerForTest += Time.deltaTime;
        }

        private void NewBall()
        {
            var ball = spawnManager.SpawnBall("ball");
            
            ball.transform.SetParent(transform);
            ball.Init(platformTransform.position, config, mainCamera, spawnManager);
            ball.OnDeactivate += healthManager.PopHeart;
        }
    }
}
