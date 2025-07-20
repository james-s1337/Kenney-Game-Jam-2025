using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> backgroundMusic; // 0 = Menu music, 1-3 = Level music corresponding to the number
    [SerializeField] private Slider volumeSlider;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetAsMainMenuMusic();

        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    // Sent from LevelManager
    public void SetNewBackgroundMusic(int index)
    {
        audioSource.clip = backgroundMusic[index];
        audioSource.Play();
    }

    public void SetAsMainMenuMusic()
    {
        audioSource.clip = backgroundMusic[0];
        audioSource.Play();
    }

    public void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
    }
}
