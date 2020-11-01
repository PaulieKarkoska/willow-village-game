using Invector.vCharacterController.AI;
using System.Collections;
using UnityEngine;

public abstract class NpcSpawner : MonoBehaviour
{
    [SerializeField]
    protected GameObject npcPrefab;
    [SerializeField]
    protected vWaypointArea waypointArea;

    public bool isSpawning { get; protected set; }
}