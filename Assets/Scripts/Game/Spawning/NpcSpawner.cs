using Invector.vCharacterController.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    public enum AllyOrEnemy
    {
        Ally,
        Enemy,
    }

    [SerializeField]
    private GameObject npcPrefab;

    [SerializeField]
    private vWaypointArea waypointArea;

    [SerializeField]
    private AllyOrEnemy allyOrEnemy;

    [SerializeField]
    private bool testMode = false;

    private List<GameObject> npcList = new List<GameObject>();
    private bool _isSpawning = false;

    private void Start()
    {
        if (testMode)
            StartSpawning();
    }

    public void StartSpawning()
    {
        if (!_isSpawning)
            StartCoroutine(SpawnLoop());

        _isSpawning = true;
    }
    public void StopSpawning()
    {
        if (_isSpawning)
            StopCoroutine(SpawnLoop());

        _isSpawning = false;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            if (allyOrEnemy == AllyOrEnemy.Ally)
                SpawnSoldier();
            else
                SpawnBandit();
            yield return new WaitForSeconds(27);
        }
    }

    private void SpawnBandit()
    {
        var bandit = Instantiate(npcPrefab, transform.position, Quaternion.identity);
        var enemy = bandit.GetComponent<v_AIController>();
        enemy.pathArea = waypointArea;

        npcList.Add(bandit);
    }

    private void SpawnSoldier()
    {
        var soldier = Instantiate(npcPrefab, transform.position, Quaternion.identity);
        var companion = soldier.GetComponent<v_AICompanion>();
        companion.onDead.AddListener(g => npcList.Remove(g.gameObject));
        companion.pathArea = waypointArea;

        npcList.Add(soldier);
    }
}