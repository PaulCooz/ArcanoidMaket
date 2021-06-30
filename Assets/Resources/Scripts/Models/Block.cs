using UnityEngine;

namespace Resources.Scripts.Models
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private int health;
    
        public BoxCollider2D blockCollider;
    
        public void Init(Vector2 size, int startHealth)
        {
            blockCollider.size = size;
            health = startHealth;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            health--;

            if (health <= 0)
            {
                Remove();
            }
        }

        private void Remove()
        {
            transform.localScale = Vector3.zero;
        }
    }
}
