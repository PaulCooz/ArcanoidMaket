using UnityEngine;

namespace Models.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private BlockManager blockManager;
        [SerializeField]
        private HealthManager healthManager;
        [SerializeField]
        private LevelLoader levelLoader;
        [SerializeField]
        private LocaleManager localeManager;

        private void Start()
        {
            localeManager.SetLocale(Locale.Ru);
            print(localeManager.GetText("startMessage"));
            
            LoadNextLevel();
        }

        public void LoadNextLevel()
        {
            blockManager.NewLevel(levelLoader.GetNextLevel());
            SetHearts();
        }

        private void SetHearts()
        {
            healthManager.SetHearts();
        }
    }
}