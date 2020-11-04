using Invector.vCharacterController.AI;
using UnityEngine;

public abstract class NpcSpawner : MonoBehaviour
{
    [SerializeField]
    protected GameObject npcPrefab;
    [SerializeField]
    protected vWaypointArea waypointArea;
    [SerializeField]
    protected float spawnDelay;
    [SerializeField]
    protected int maxConcurrent;
}