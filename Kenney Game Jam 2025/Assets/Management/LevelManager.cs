using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int powerCellGoal; // How many power cells you must have before you can blast off!
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private int nextLevelNum;
    [SerializeField] private PowerCellManager powerCellManager;

    private int powerCellsCollected;
    private float startTime;

    public UnityEvent OnLevelCompleted;
    public UnityEvent<int> OnLevelStart, OnPowerCellCollected, OnLevelClear, OnLevelLoad;
    public UnityEvent<LevelManager> OnLevelEnable;

    private Player player;

    private void OnEnable()
    {
        OnLevelEnable?.Invoke(gameObject.GetComponent<LevelManager>());
        StartLevel();
    }

    public void AddPowerCell()
    {
        powerCellsCollected++;
        // Save max for gameObject.transform.parent.gameObject.name
        // Check if max power cells in this level can be saved
        OnPowerCellCollected?.Invoke(powerCellsCollected);
        if (powerCellsCollected >= powerCellGoal)
        {
            OnLevelCompleted?.Invoke();
        }
    }

    public void StartLevel()
    {
        ClearLevel();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        startTime = Time.time;
        OnLevelClear?.Invoke(nextLevelNum);
        OnLevelLoad?.Invoke(nextLevelNum-1);
        OnLevelStart?.Invoke(powerCellGoal);  
    }

    public void LoadNextLevel()
    {
        if (gameObject.transform.parent.gameObject.name == "Level3")
        {
            return;
        }
        ClearLevel();
        gameObject.transform.parent.gameObject.SetActive(false);
        nextLevel.SetActive(true);
    }

    public void ClearLevel()
    {
        Debug.Log(gameObject.transform.parent.gameObject.name + "cells" + " : " + PlayerPrefs.GetInt(gameObject.transform.parent.gameObject.name + "cells", 0));
        Debug.Log(gameObject.transform.parent.gameObject.name + "time" + " : " + PlayerPrefs.GetFloat(gameObject.transform.parent.gameObject.name + "time", 0));

        // Save max for gameObject.transform.parent.gameObject.name
        if (PlayerPrefs.GetInt(gameObject.transform.parent.gameObject.name + "cells", 0) < powerCellsCollected)
        {
            PlayerPrefs.SetInt(gameObject.transform.parent.gameObject.name + "cells", powerCellsCollected);
        }

        powerCellManager.ResetPowerCellManager();
        powerCellsCollected = 0;
    }

    public void RecordCompletionTime()
    {
        float completionTime = Time.time - startTime;
        
        // Save completion time for gameObject.transform.parent.gameObject.name
        if (completionTime < PlayerPrefs.GetFloat(gameObject.transform.parent.gameObject.name + "time", 0) || PlayerPrefs.GetFloat(gameObject.transform.parent.gameObject.name + "time", 0) == 0)
        {
            PlayerPrefs.SetFloat(gameObject.transform.parent.gameObject.name + "time", completionTime);
        }  
    }
}
