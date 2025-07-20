using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private int level;
    public TextMeshProUGUI powerCellText;
    public TextMeshProUGUI timeText;
    private void Start()
    {
        UpdatePowerCellText();
        UpdateTimeText();
    }
    private void UpdatePowerCellText()
    {
        powerCellText.text = PlayerPrefs.GetInt("Level" + level.ToString() + "cells", 0).ToString();
    }

    private void UpdateTimeText()
    {
        float rawTime = PlayerPrefs.GetFloat("Level" + level.ToString() + "time", 0);
        if (rawTime == 0f)
        {
            timeText.text = "Not Cleared";
            return;
        }
        int minutes = Mathf.FloorToInt(rawTime / 60);
        int seconds = Mathf.FloorToInt(rawTime - (minutes * 60));

        if (seconds < 10)
        {
            timeText.text = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            timeText.text = minutes.ToString() + ":" + seconds.ToString();
        }  
    }

    public void UpdateScores()
    {
        UpdatePowerCellText();
        UpdateTimeText();
    }
}
