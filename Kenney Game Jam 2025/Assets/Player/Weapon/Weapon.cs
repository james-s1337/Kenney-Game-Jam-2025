using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType weaponName; // Name of weapon, for character change
    [SerializeField] private float cooldown; // How long you have to wait before you can fire another projectile
    [SerializeField] private float burstCooldown; // How fast the burst is fired (only for Burst)
    [SerializeField] private int projNum; // How many projectiles are shot out of this weapon

    [SerializeField] private AudioSource shootSound;

    public Slider SFXSlider;

    private int currentProjNum; // Current projectile being used in the projectile list
    private float startTime; // When this weapon was last fired

    public List<GameObject> projectiles; // Pre-loaded assets for the projectiles

    private void Awake()
    {
        currentProjNum = 0;
    }

    private void Start()
    {
        SFXSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }

    private void ChangeSFXVolume()
    {
        shootSound.volume = SFXSlider.value;
    }

    public WeaponType GetWeapType()
    {
        ChangeSFXVolume();
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
            shootSound.Play();
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
