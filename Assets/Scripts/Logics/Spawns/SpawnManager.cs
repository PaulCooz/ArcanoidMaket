using Logics.Balls;
using Logics.Blocks;
using Logics.Healths;
using UnityEngine;

namespace Logics.Spawns
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        private PoolBlockManager poolBlockManager;
        [SerializeField]
        private PoolBallManager poolBallManager;
        [SerializeField]
        private PoolHeartManager poolHeartManager;

        public Block GetBlock() => poolBlockManager.GetFromPool();
        public Ball GetBall() => poolBallManager.GetFromPool();
        public Heart GetHeart() => poolHeartManager.GetFromPool();

        public void Remove(Block block) => poolBlockManager.SetToPool(block);
        public void Remove(Ball ball) => poolBallManager.SetToPool(ball);
        public void Remove(Heart heart) => poolHeartManager.SetToPool(heart);
    }
}
