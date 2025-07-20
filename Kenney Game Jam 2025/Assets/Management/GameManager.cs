using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPopup;
    [SerializeField] private GameObject victoryPopup;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameplayUI;

    [SerializeField] private AudioSource gameOverSound;

    public LevelManager currentLevel { get; private set; }

    public Slider SFXSlider;

    public UnityEvent OnLevelSet;

    private void Start()
    {
        SFXSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }

    private void ChangeSFXVolume()
    {
        gameOverSound.volume = SFXSlider.value;
    }

    private bool CheckIfGameEndByUI()
    {
        if (victoryPopup.activeSelf || gameOverPopup.activeSelf)
        {
            return true;
        }
        if (!gameplayUI.activeSelf)
        {
            return true;
        }
        return false;
    }

    public void Defeat()
    {
        ChangeSFXVolume();
        Time.timeScale = 0;
        gameOverSound.Play();
        gameOverPopup.SetActive(true);
    }

    public void Victory()
    {
        Time.timeScale = 0;
        victoryPopup.SetActive(true);
    }

    public void PauseGame()
    {
        if (CheckIfGameEndByUI())
        {
            return;
        }
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        if (CheckIfGameEndByUI())
        {
            return;
        }
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ClearUI()
    {
        victoryPopup.SetActive(false);
        gameOverPopup.SetActive(false);
        ResumeGame();
    }

    public void SetCurrentLevel(LevelManager newLevel)
    {
        currentLevel = newLevel;
        OnLevelSet?.Invoke();
    }

    public void RestartCurrentLevel()
    {
        currentLevel.StartLevel();
    }

    public void LoadNextLevel()
    {
        currentLevel.LoadNextLevel();
    }
}
