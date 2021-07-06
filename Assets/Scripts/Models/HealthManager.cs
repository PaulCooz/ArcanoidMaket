using UnityEngine;

namespace Models.Managers
{
    public class HealthManager : MonoBehaviour
    {
        private const int Null = -1;
        
        private Heart[,] _hearts;
        private int _activeHearts = Null;

        [SerializeField]
        private int startHeartsCount;
        [SerializeField]
        private GridOfHearts gridOfHearts;
        [SerializeField] 
        private LocaleManager localeManager;

        public void SetHearts()
        {
            if (_activeHearts != Null)
            {
                while (_activeHearts > 0)
                {
                    _hearts[0, --_activeHearts].Pop();
                }
            }

            var heartTags = new string[startHeartsCount];
            for (var i = 0; i < startHeartsCount; i++)
            {
                heartTags[i] = "heart";
            }
            
            _hearts = gridOfHearts.NewGrid(1, startHeartsCount, heartTags);
            _activeHearts = startHeartsCount;
        }
        
        public void PopHeart()
        {
            if (_activeHearts <= 0) return;
            
            _activeHearts--;
            _hearts[0, _activeHearts].Pop();
            
            if (_activeHearts <= 0)
            {
                print(localeManager.GetText("gameOverMessage"));
            }
        }
    }
}
