using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class Spaceship : MonoBehaviour, IDamageable
{
    [SerializeField] private List<SpriteRenderer> hearts;

    public int hp { get; private set; }
    public UnityEvent OnDestroyed;
    public bool canEscape { get; private set; }
    private void Start()
    {
        hp = 3; // Always 3
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 2)
        {
            hearts[2].color = Color.black;

            if (hp <= 1)
            {
                hearts[1].color = Color.black;

                if (hp <= 0)
                {
                    hearts[0].color = Color.black;
                }
            }
        }

        if (hp <= 0)
        {
            OnDestroyed?.Invoke();
        }
    }

    public void EnableEscape()
    {
        canEscape = true;
    }

    public void ResetSpaceship()
    {
        canEscape = false;
        hp = 3;
        hearts[0].color = Color.white;
        hearts[1].color = Color.white;
        hearts[2].color = Color.white;
    }
}
