using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private List<LevelDisplay> levelDisplays;
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        if (gameManager.currentLevel)
        {
            gameManager.currentLevel.ClearLevel();
        }
        foreach (LevelDisplay display in levelDisplays)
        {
            display.UpdateScores();
        }
    }
}
