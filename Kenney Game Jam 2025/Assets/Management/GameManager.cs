using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPopup;
    [SerializeField] private GameObject victoryPopup;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameplayUI;

    public LevelManager currentLevel { get; private set; }

    public UnityEvent OnLevelSet;

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
        Time.timeScale = 0;
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
