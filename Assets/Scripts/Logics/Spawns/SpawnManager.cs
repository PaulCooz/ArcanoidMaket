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

        public Blocks.Block GetBlock() => poolBlockManager.GetFromPool();
        public Balls.Ball GetBall() => poolBallManager.GetFromPool();
        public Heart GetHeart() => poolHeartManager.GetFromPool();

        public void Remove(Blocks.Block block) => poolBlockManager.SetToPool(block);
        public void Remove(Balls.Ball ball) => poolBallManager.SetToPool(ball);
        public void Remove(Heart heart) => poolHeartManager.SetToPool(heart);
    }
}
