using UnityEngine;

public class SeedCollectable : MonoBehaviour
{
    public int value = 1;
    [SerializeField]
    private GameObject _effectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var mgr = other.GetComponent<CollectableManager>();
            mgr.AddSeeds(value);
            Instantiate(_effectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}