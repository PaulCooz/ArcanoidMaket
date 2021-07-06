using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu(fileName = "gameConfig", menuName = "new game config")]
    public class GameConfig : ScriptableObject
    {
        public readonly string EmptyCellName = "EmptyCell";

        [SerializeField] [Range(0, 1)] 
        public float platformWidth;
        [SerializeField] [Range(0, 1)] 
        public float platformHeight;

        [SerializeField]
        public float pushBallForce = 100f;
    }
}
