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

    public static int waveEnemyCount { get; private set; }
    public static int routedEnemyCount { get; private set; }
    public static int enemiesSpawned { get; private set; }

    [SerializeField]
    private int spawnDelay = 30;
    [SerializeField]
    private int maxConcurrent = 10;
    private int concurrentCount = 0;

    [Header("Testing")]
    [SerializeField]
    private bool testMode;

    private void Start()
    {
        if (testMode)
            StartSpawning(30);
    }

    public void StartSpawning(int enemiesPerWave)
    {
        waveEnemyCount = enemiesPerWave;
        routedEnemyCount = 0;
        concurrentCount = 0;
        OnSpawningStarted?.Invoke();

        StartCoroutine(SpawnLoop());
        isSpawning = true;
    }

    private IEnumerator SpawnLoop()
    {
        while (waveEnemyCount > enemiesSpawned)
        {
            yield return new WaitForSeconds(1);
            if (concurrentCount < maxConcurrent)
                SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay - 1);
        }
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
        });
        concurrentCount++;
        enemiesSpawned++;

        enemy.pathArea = waypointArea;
    }
}