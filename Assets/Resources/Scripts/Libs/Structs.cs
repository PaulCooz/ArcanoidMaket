using UnityEngine;

namespace Resources.Scripts.Libs
{
    [System.Serializable]
    public struct LevelData
    {
        public int height;
        public int width;
        public int[] data;
    }

    [System.Serializable]
    public struct Pool
    {
        public int tag;
        public GameObject pref;
        public int poolSize;
    }
}