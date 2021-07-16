using Libs.Interfaces;
using Logics.Spawns;
using UnityEngine;
using View;

namespace Logics
{
    public class Block : MonoBehaviour, IPoolable
    {
        private SpawnManager _spawnManager;
        
        [SerializeField]
        private int hitPoint;

        public BlockView blockView;
        public int id;

        public void Init(SpawnManager spawnManager)
        {
            _spawnManager = spawnManager;
            hitPoint = 2;
        }
        
        public void Touch(int damage)
        {
            hitPoint -= damage;

            if (hitPoint <= 0) Remove();
        }

        public void Remove()
        {
            _spawnManager.Remove(this);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}