using Libs;
using UnityEngine;

namespace Models
{
    public class Block : MonoBehaviour, IPoolable
    {
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