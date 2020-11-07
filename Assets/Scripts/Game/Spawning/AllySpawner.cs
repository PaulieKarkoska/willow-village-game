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

    public static int weaponLevel = 0;
    public static int armorLevel = 0;

    public static void UpgradeArmor()
    {
        armorLevel++;
    }

    [SerializeField]
    private KeepChest keepChest;

    public int costPerSoldier { get; set; } = 15;

    private void Start()
    {
        wController = GameObject.Find("WaveController").GetComponent<WaveController>();
        if (!keepChest)
            GameObject.Find("Keep Chest").GetComponent<KeepChest>();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (wController.waveIsInProgress
                && alliesSpawned < maxConcurrent
                && keepChest.HasEnoughMoney(costPerSoldier))
            {
                keepChest.RemoveMoney(costPerSoldier);
                SpawnAlly();
            }
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