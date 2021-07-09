using Loaders;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public void SetLevelPack(int packId)
    {
        DataHolder.LevelPack = packId;
    }

    public void SetLocale(Locale locale)
    {
        DataHolder.CurrentLocale = locale;
    }
}
