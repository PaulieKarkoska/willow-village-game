using Invector.vCharacterController.AI;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject soldierPrefab;

    private List<GameObject> soldiers = new List<GameObject>();
    private bool _isSpawning = false;

    private void Start()
    {
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
            SpawnSoldier();
            yield return new WaitForSeconds(27);
        }
    }

    private void SpawnSoldier()
    {
        var soldier = Instantiate(soldierPrefab, transform.position, Quaternion.identity);
        var companion = soldier.GetComponent<v_AICompanion>();
        companion.onDead.AddListener(g => soldiers.Remove(g.gameObject));
        soldiers.Add(soldier);
    }
}