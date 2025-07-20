using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType weaponName; // Name of weapon, for character change
    [SerializeField] private float cooldown; // How long you have to wait before you can fire another projectile
    [SerializeField] private float burstCooldown; // How fast the burst is fired (only for Burst)
    [SerializeField] private int projNum; // How many projectiles are shot out of this weapon

    private int currentProjNum; // Current projectile being used in the projectile list
    private float startTime; // When this weapon was last fired

    public List<GameObject> projectiles; // Pre-loaded assets for the projectiles

    private void Awake()
    {
        currentProjNum = 0;
    }

    public WeaponType GetWeapType()
    {
        return weaponName;
    }

    public bool CanFire()
    {
        return Time.time - startTime >= cooldown;
    }

    public void Fire()
    {
        startTime = Time.time;
        StartCoroutine(StartFiringSequence());
    }

    private IEnumerator StartFiringSequence()
    {
        for (int i = 0; i < projNum; i++)
        {
            projectiles[currentProjNum].SetActive(false);
            projectiles[currentProjNum].SetActive(true); // Projectile script will have a timer that will make it inactive when it is activated
            // Play shoot sound

            currentProjNum++;
            if (currentProjNum >= projectiles.Count)
            {
                currentProjNum = 0;
            }

            if (burstCooldown > 0)
            {
                yield return new WaitForSeconds(burstCooldown);
            }
        }
        
    }
}

public enum WeaponType
{
    Single,
    Burst,
    Goop
}
