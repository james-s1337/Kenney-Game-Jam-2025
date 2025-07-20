using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<GameObject> enemySpawns;
    [SerializeField] private float spawnRate;

    private float startTime;
    private float timeSinceLastSpawn;

    private const int minEnemySpawns = 1;
    private const int maxEnemySpawns = 3;
    private int spawnRateClimbs = 0; // How many times the spawn rate has increased, max is 5
    private const int maxSpawnRateClimbs = 5; // Max times the spawn rate can increase
    private const float spawnRateClimb = 0.15f; // Only happens 5 times
    private float currentSpawnRate;

    private bool canStartSpawning;

    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        canStartSpawning = false;
        currentSpawnRate = spawnRate;
        spawnRateClimbs = 0;
        StartCoroutine("GraceTime");
    }

    public void SetStartTime()
    {
        timeSinceLastSpawn = 0;
        startTime = Time.time;
    }

    private void Update()
    {
        if (!canStartSpawning)
        {
            return;
        }

        if (timeSinceLastSpawn == 0 || Time.time - timeSinceLastSpawn >= currentSpawnRate)
        {
            timeSinceLastSpawn = Time.time;
            StartCoroutine("SpawnWave");
        }

        if (Mathf.RoundToInt(Time.time - startTime) % 8 == 0 && spawnRateClimbs < maxSpawnRateClimbs)
        {
            spawnRateClimbs++;
            currentSpawnRate -= spawnRateClimb;
        }
    }

    private IEnumerator SpawnWave()
    {
        int numOfEnemies = Random.Range(minEnemySpawns, maxEnemySpawns+1);
        int enemyType;
        if (numOfEnemies == 1)
        {
            enemyType = 0; // 0 will always be where the tanky monster is
        }
        else
        {
            enemyType = Random.Range(1, enemies.Count);
        }
        int spawnNum = Random.Range(0, enemySpawns.Count);

        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemies[enemyType], enemySpawns[spawnNum].transform.position, Quaternion.identity, enemySpawns[spawnNum].transform);
            newEnemy.SetActive(true);
            if (spawnNum % 2 != 0)
            {
                newEnemy.GetComponent<Enemy>().ChangeStartDirection();
            }
            yield return new WaitForSeconds(0.4f);
        }
    }

    private IEnumerator GraceTime()
    {
        foreach (GameObject enemySpawn in enemySpawns)
        {
            foreach (Transform child in enemySpawn.transform)
            {
                Destroy(child.gameObject);
            }
        }
        yield return new WaitForSeconds(3f);
        SetStartTime();
        canStartSpawning = true;
    }
}
