using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu(fileName = "gameConfig", menuName = "new game config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] 
        public float uiZone = 0.9f;
        
        [SerializeField] [Range(0, 1)] 
        public float platformWidth;
        [SerializeField] [Range(0, 1)] 
        public float platformHeight;

        [SerializeField] 
        public float ballVelocity = 5;
        [SerializeField] [Range(0, 90)]
        public float minAngle = 20;
        [SerializeField] 
        public float ballStartForce = 300;
        [SerializeField]
        public float startBallHeight = -3;
    }
}
