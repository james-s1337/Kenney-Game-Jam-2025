using TMPro;
using UnityEngine;

public class PowerCellDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI textShadow;

    private int requiredPowerCellCount;
    public void InitiateCount(int requiredCellCount)
    {
        requiredPowerCellCount = requiredCellCount;
        text.text = "0/" + requiredCellCount.ToString();
        textShadow.text = text.text;
    }
    public void UpdatePowerCellCount(int count)
    {
        text.text = count.ToString() + "/" + requiredPowerCellCount.ToString();
        textShadow.text = text.text;
    }
}
