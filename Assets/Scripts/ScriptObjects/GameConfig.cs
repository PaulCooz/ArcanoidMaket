using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu(fileName = "gameConfig", menuName = "new game config")]
    public class GameConfig : ScriptableObject
    {
        [Header("\tUI")] [Space]
        [SerializeField]
        public float uiZone = 0.9f;
        
        [Header("\tENERGY")] [Space]
        [SerializeField] 
        public int maxEnergy;
        [SerializeField] 
        public double energyPerSecond;
        
        [Header("\tPLATFORM")] [Space]
        [SerializeField] [Range(0, 1)] 
        public float platformWidth;
        [SerializeField] [Range(0, 1)] 
        public float platformHeight;
        [SerializeField] [Min(0)] 
        public float platformSpeed;

        [Header("\tBALL")] [Space]
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
