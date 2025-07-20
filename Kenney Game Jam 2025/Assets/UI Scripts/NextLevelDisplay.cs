using TMPro;
using UnityEngine;

public class NextLevelDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    public void ChangeNextLevelText(int lvl)
    {
        text.text = "Level " + lvl.ToString();
    }
}
