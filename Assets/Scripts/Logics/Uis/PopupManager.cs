using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] 
    private Popup gameOver;

    public void Awake()
    {
        gameOver.Hide();
        // gameOver.gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        // gameOver.gameObject.SetActive(true);
        gameOver.Show();
    }
}
