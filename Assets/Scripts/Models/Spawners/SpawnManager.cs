using UnityEngine;

namespace Models.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        private PoolBlockManager poolBlockManager;
        [SerializeField]
        private PoolBallManager poolBallManager;
        [SerializeField]
        private PoolHeartManager poolHeartManager;

        public Block SpawnBlock(string blockTag)
        {
            var block = poolBlockManager.GetFromPool(blockTag);
            return block;
        }

        public Ball SpawnBall(string ballTag)
        {
            var ball = poolBallManager.GetFromPool(ballTag);
            return ball;
        }
        
        public Heart SpawnHeart(string heartTag)
        {
            var heart = poolHeartManager.GetFromPool(heartTag);
            return heart;
        }

        public void Remove(Block block)
        {
            poolBlockManager.SetToPool(block);
        }
        
        public void Remove(Ball ball)
        {
            poolBallManager.SetToPool(ball);
        }

        public void Remove(Heart heart)
        {
            poolHeartManager.SetToPool(heart);
        }
    }
}
