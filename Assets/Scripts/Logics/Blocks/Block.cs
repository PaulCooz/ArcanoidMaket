using System;
using Libs;
using Libs.Interfaces;
using Logics.Spawns;
using UnityEngine;
using View;

namespace Logics.Blocks
{
    public class Block : MonoBehaviour, IPoolable
    {
        private SpawnManager _spawnManager;
        
        [SerializeField]
        private int hitPoint;

        public BlockView blockView;
        public int id;
        
        public event Action<int> OnDeactivate;

        public void Init(SpawnManager spawnManager)
        {
            _spawnManager = spawnManager;
            hitPoint = 2;
        }
        
        public void Touch()
        {
            hitPoint--;

            if (hitPoint <= 0) Remove();
        }

        public void Remove()
        {
            _spawnManager.Remove(this);
        }

        public void Activate()
        {
            OnDeactivate = null;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            
            if (EventsAndStates.IsGameRun) OnDeactivate?.Invoke(id);
        }
    }
}