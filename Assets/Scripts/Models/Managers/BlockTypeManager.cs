using System;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Managers
{
    public enum BlockType
    {
        Empty,
        Common,
        Unbreakable,
        Bomb,
        ChainBomb,
        BallSpeedUp,
        BallSpeedDown,
        PlatformDilator,
        PlatformNarrower,
        FuryBall,
        BallsAdder,
        VerticalBomb,
        HorizontalBomb,
        PlatformSpeedUp,
        PlatformSpeedDown,
        HeartAdder,
        HeartRemover
    }
    
    public class BlockTypeManager : MonoBehaviour
    {
        private int _blockTypeQuantity;
        
        [Serializable]
        public struct BlockData
        {
            public int hitPoint;
            public Color color;
            public UnityEvent<Block> endAction;
            public UnityEvent<BlockType> bulletAction;
        }

        [SerializeField]
        private BlockData[] blockData;

        private void Awake()
        {
            _blockTypeQuantity = Enum.GetValues(typeof(BlockType)).Length;

            if (blockData.Length != _blockTypeQuantity)
            {
                Debug.LogWarning("blocks data error: " + blockData.Length + " != " + _blockTypeQuantity);
            }
        }

        public BlockData GetData(BlockType blockType)
        {
            return blockData[(int) blockType];
        }
    }
}
