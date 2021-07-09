using Loaders;
using TMPro;
using UnityEngine;

public class Pack : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI packTitle;
    [SerializeField]
    private int packId;
    [SerializeField] 
    private DataHandler dataHandler;

    private void Start()
    {
        packTitle.text = LocaleManager.GetText("packName" + packId);
    }

    public void Pushed()
    {
        dataHandler.SetLevelPack(packId);
    }
}
