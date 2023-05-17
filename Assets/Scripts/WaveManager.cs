using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public GameObject enemyPrefab;
    private int currentWave = 0;
    private List<GameObject> currentEnemies = new List<GameObject>();
    public static WaveManager instance;

    private void Start()
    {
        if (waves.Length > 0)
        {
            StartWave();
            instance = this;
        }
    }

    private void StartWave()
    {
        StartCoroutine(SpawnEnemies());
    }

    public int GetWave()
    {
        return currentWave + 1;
    }

    public int GetMaxWave()
    {
        return waves.Length;
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < waves[currentWave].numberOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            currentEnemies.Add(newEnemy);
            yield return new WaitForSeconds(waves[currentWave].spawnDelay);
        }
    }

    public void EnemyDied(GameObject enemy)
    {
        currentEnemies.Remove(enemy);

        if (currentEnemies.Count == 0)
        {
            if (currentWave < waves.Length - 1)
            {
                currentWave++;
                StartWave();
            }
            else
            {
                // All waves completed
                GameManager gameManager = FindObjectOfType<GameManager>();
                gameManager.AllWavesCompleted();
            }
        }
    }
}

[System.Serializable]
public struct Wave
{
    public int numberOfEnemies;
    public float spawnDelay;
}