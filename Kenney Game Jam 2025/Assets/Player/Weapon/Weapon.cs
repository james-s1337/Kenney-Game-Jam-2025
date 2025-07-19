using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int projNum; // How many projectiles are shot out of this weapon
    private int currentProjNum; // Current projectile being used in the projectile list

    public List<GameObject> projectiles; // 6

    private void Awake()
    {
        currentProjNum = 0;
    }

    public void Fire()
    {
        for (int i = currentProjNum)
        {

        }
    }
}
