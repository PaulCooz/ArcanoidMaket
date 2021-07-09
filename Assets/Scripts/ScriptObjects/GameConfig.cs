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
        public float ballForce = 5;
        [SerializeField]
        public float startBallHeight = -3;
    }
}
