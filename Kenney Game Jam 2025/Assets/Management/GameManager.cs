using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPopup;
    [SerializeField] private GameObject victoryPopup;

    public LevelManager currentLevel { get; private set; }

    public void Defeat()
    {
        PauseGame();
        gameOverPopup.SetActive(true);
    }

    public void Victory()
    {
        PauseGame();
        victoryPopup.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
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
