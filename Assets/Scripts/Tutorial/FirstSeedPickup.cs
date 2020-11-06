using UnityEngine;

public class FirstSeedPickup : MonoBehaviour
{
    public delegate void FirstSeedCollected();
    public static event FirstSeedCollected OnFirstSeedCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnFirstSeedCollected?.Invoke();
    }
}