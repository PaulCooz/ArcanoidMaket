using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public void SetLevelPack(TextAsset[] packLevels)
    {
        DataHolder.Levels = packLevels;
    }
}
