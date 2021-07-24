using UnityEngine;
using View;

namespace Libs
{
    public abstract class Popup : MonoBehaviour
    {
        public abstract void Init(Foreground foreground);
        public abstract void Show();
        public abstract void Hide();
    }
}