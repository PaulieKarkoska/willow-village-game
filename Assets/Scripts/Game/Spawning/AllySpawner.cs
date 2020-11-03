using Invector.vCharacterController.AI;
using System.Collections;
using UnityEngine;

public class AllySpawner : NpcSpawner
{
    public int alliesSpawned { get; private set; }

    public delegate void AllySpawned(int alliesRemaining);
    public static event AllySpawned OnAllySpawned;

    public delegate void AllyKilled(int alliesRemaining);
    public static event AllyKilled OnAllyKilled;

    [SerializeField]
    private int spawnDelay = 30;
    [SerializeField]
    private int maxConcurrent = 10;

    [Header("Testing")]
    [SerializeField]
    private bool testMode;

    private void Start()
    {
        if (testMode)
            StartSpawning();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnLoop());

        isSpawning = true;
    }
    public void StopSpawning()
    {
        if (isSpawning)
            StopCoroutine(SpawnLoop());

        isSpawning = false;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (alliesSpawned < maxConcurrent)
                SpawnAlly();
            yield return new WaitForSeconds(spawnDelay - 1);
        }
    }

    private void SpawnAlly()
    {
        var bandit = Instantiate(npcPrefab, transform.position, Quaternion.identity);
        var enemy = bandit.GetComponent<v_AIController>();
        enemy.onDead.AddListener(g =>
        {
            alliesSpawned--;
            OnAllyKilled?.Invoke(alliesSpawned);
        });
        alliesSpawned++;
        OnAllySpawned?.Invoke(alliesSpawned);
        enemy.pathArea = waypointArea;
    }
}