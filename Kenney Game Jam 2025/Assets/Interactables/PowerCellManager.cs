using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PowerCellManager : MonoBehaviour
{
    public List<GameObject> powerCells;

    private int currentPowerCellIndex;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPowerCellIndex = 0;
        foreach (GameObject cell in powerCells)
        {
            cell.SetActive(false);
        }
        SpawnNewPowerCell();
    }

    public void SpawnNewPowerCell()
    {
        powerCells[currentPowerCellIndex].SetActive(false);
        currentPowerCellIndex += 1;
        if (currentPowerCellIndex >= powerCells.Count)
        {
            currentPowerCellIndex = 0;
        }
        powerCells[currentPowerCellIndex].SetActive(true);
    }

    public void ResetPowerCellManager()
    {
        powerCells[currentPowerCellIndex].SetActive(false);
        currentPowerCellIndex = 0;
    }
}
