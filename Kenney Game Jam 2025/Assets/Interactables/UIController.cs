using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject gameplayUI;
    public void ShowGameplayUI()
    {
        gameplayUI.SetActive(true);
    }

    public void HideGameplayUI()
    {
        gameplayUI.SetActive(false);
    }
}
