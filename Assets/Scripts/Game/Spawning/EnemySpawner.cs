using Invector.vCharacterController.AI;
using System.Collections;
using UnityEngine;

public class EnemySpawner : NpcSpawner
{
    public delegate void Started();
    public static event Started OnSpawningStarted;

    public delegate void EnemySpawned();
    public static event EnemySpawned OnEnemySpawned;

    public delegate void EnemyKilled(int routed, int total);
    public static event EnemyKilled OnEnemyKilled;

    public delegate void AllEnemiesKilled();
    public static event AllEnemiesKilled OnAllEnemiesKilled;

    public delegate void SpawningCompleted();
    public static event SpawningCompleted OnSpawningCompleted;

    public static int waveEnemyCount { get; private set; }
    public static int routedEnemyCount { get; private set; }
    public static int enemiesSpawned { get; private set; }

    public static int concurrentCount { get; private set; } = 0;

    public static WaveInfo WaveInfo { get; set; }
    public void StartSpawning()
    {
        wController.waveIsInProgress = true;
        waveEnemyCount = WaveInfo.waveEnemyCount;
        enemiesSpawned = 0;
        routedEnemyCount = 0;
        concurrentCount = 0;

        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        OnSpawningStarted?.Invoke();
        while (waveEnemyCount > enemiesSpawned)
        {
            yield return new WaitForSeconds(1);
            if (concurrentCount < maxConcurrent)
                SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay - 1);
        }
        OnSpawningCompleted?.Invoke();
    }

    private void SpawnEnemy()
    {
        var bandit = Instantiate(npcPrefab, transform.position, Quaternion.identity);
        var enemy = bandit.GetComponent<v_AIController>();
        enemy.onDead.AddListener(g =>
        {
            routedEnemyCount++;
            concurrentCount--;
            OnEnemyKilled?.Invoke(routedEnemyCount, waveEnemyCount);
            if (routedEnemyCount == WaveInfo.waveEnemyCount)
            {
                OnAllEnemiesKilled?.Invoke();
                wController.waveIsInProgress = false;
                wController.StartWaveCountdown();
            }

        });
        concurrentCount++;
        OnEnemySpawned?.Invoke();
        enemiesSpawned++;

        enemy.pathArea = waypointArea;
    }
}