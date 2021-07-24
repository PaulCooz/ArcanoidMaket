using Models;
using UnityEngine;

namespace View
{
    public class ForegroundManager : MonoBehaviour
    {
        [SerializeField] 
        private Foreground foreground;

        private void Awake()
        {
            EventsAndStates.OnPackDone += foreground.Show;
        }

        private void OnDestroy()
        {
            EventsAndStates.OnPackDone -= foreground.Show;
        }
    }
}