using Dataers;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PackImage : MonoBehaviour
    {
        [SerializeField] 
        private Image image;
        
        private void Start()
        {
            image.sprite = DataHolder.PackImage.sprite;
        }
    }
}
