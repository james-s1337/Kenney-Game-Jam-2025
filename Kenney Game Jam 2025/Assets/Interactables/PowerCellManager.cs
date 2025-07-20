using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PowerCellManager : MonoBehaviour
{
    public List<GameObject> powerCells;

    private int currentPowerCellIndex;

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
