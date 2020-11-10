using Invector.vCharacterController.AI;
using UnityEngine;

public abstract class NpcSpawner : MonoBehaviour
{
    [SerializeField]
    protected GameObject npcPrefab;
    [SerializeField]
    protected vWaypointArea waypointArea;
    [SerializeField]
    protected float spawnDelay = 2;
    [SerializeField]
    protected int maxConcurrent = 15;
    [SerializeField]
    protected WaveController wController;
}