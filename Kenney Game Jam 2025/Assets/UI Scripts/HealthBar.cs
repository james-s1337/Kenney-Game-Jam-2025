using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private List<Image> hearts;
    private void Start()
    {
        hearts[0].color = Color.white;
        hearts[1].color = Color.white;
        hearts[2].color = Color.white;
    }
    public void UpdateHealthDisplay(int hp)
    {
        if (hp <= 2)
        {
            hearts[2].color = Color.black;

            if(hp <= 1)
            {
                hearts[1].color = Color.black;

                if (hp <= 0)
                {
                    hearts[0].color = Color.black;
                }
            }
        }
    }

    public void ResetHealthBar(int placeholder)
    {
        hearts[0].color = Color.white;
        hearts[1].color = Color.white;
        hearts[2].color = Color.white;
    }
}
