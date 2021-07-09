using Loaders;
using TMPro;
using UnityEngine;

public class GameWinPopup : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI titleText;

    private void Start()
    {
        titleText.text = LocaleManager.GetText("gameWinPopupTitle");
    }
}
