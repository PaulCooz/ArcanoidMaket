using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScriptObjects
{
    [Serializable]
    public struct LevelSet
    {
        public TextAsset[] levels;
    }
    
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
        [SerializeField] 
        public float maxVelocity;
        [SerializeField] 
        public float velocityInc;
        [SerializeField] [Range(0, 90)]
        public float minAngle = 20;
        [SerializeField] 
        public float ballStartForce = 300;
        [SerializeField]
        public float startBallHeight = -3;
        [SerializeField] 
        public int ballDamage;

        [Header("\tPACKS")] [Space]
        [SerializeField]
        public LevelSet[] packs;


        [Header("\tBONUSES")] [Space] 
        [SerializeField]
        public float ballSpeedTime = 5;
        [SerializeField]
        public float ballFuryTime = 5;
        [SerializeField] 
        public float platformWidthTime = 5;
        [SerializeField] 
        public float platformSpeedTime = 5;
        [SerializeField] 
        public float boomTime = 0.1f;
    }
}
