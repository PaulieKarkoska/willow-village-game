using UnityEngine;

public class CoinCollectable : MonoBehaviour
{
    public int value = 1;
    [SerializeField]
    private GameObject _effectPrefab;

    private void Start()
    {
        Destroy(gameObject, 120);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var mgr = other.GetComponent<CollectableManager>();
            mgr.AddMoney(value);
            Instantiate(_effectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}