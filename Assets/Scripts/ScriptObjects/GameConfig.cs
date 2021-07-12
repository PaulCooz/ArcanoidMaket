using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu(fileName = "gameConfig", menuName = "new game config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] [Range(0, 1)] 
        public float platformWidth;
        [SerializeField] [Range(0, 1)] 
        public float platformHeight;
        [SerializeField] 
        public float platformMoveTime;

        [SerializeField] 
        public float ballVelocity = 5;
        [SerializeField] 
        public float minAngleCoeff = 5;
        [SerializeField] 
        public float maxAngleCoeff = 10;
        [SerializeField] 
        public float ballStartForce = 300;
        [SerializeField]
        public float startBallHeight = -3;
    }
}
