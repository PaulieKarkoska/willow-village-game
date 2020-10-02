using UnityEngine;

public class FarmPlot : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField]
    private Collider farmerCollider;

    [Header("Farming")]
    [SerializeField]
    private float plantingDistance = 1.5f;
    [SerializeField]
    private GameObject _cropPrefab;
    private GameObject _cropInstance;

    private CollectableManager _playerInventory;

    void Start()
    {
        if (!farmerCollider)
            Debug.Log("There is no farmer collider");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player is staying in the farm plot");
            if (_cropInstance && _playerInventory.HasEnoughSeeds(1))
            {
                if (_cropInstance.GetComponent<Crop>().isPlanted)
                {
                    _cropInstance = Instantiate(_cropPrefab, CalculateCropPosition(other.transform), other.transform.rotation);
                    _cropInstance.GetComponent<Crop>().playerInventory = _playerInventory;
                }
                else
                {
                    _cropInstance.transform.position = CalculateCropPosition(other.transform);
                    _cropInstance.transform.rotation = other.transform.rotation;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInventory = other.GetComponent<CollectableManager>();

            Debug.Log("player entered farm plot");

            if (!_cropInstance && _playerInventory.HasEnoughSeeds(1))
            {
                _cropInstance = Instantiate(_cropPrefab, CalculateCropPosition(other.transform), other.transform.rotation);
                _cropInstance.GetComponent<Crop>().playerInventory = _playerInventory;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player exited farm plot");

            if (_cropInstance)
            {
                if (_cropInstance.GetComponent<Crop>().isPlanted)
                    _cropInstance = null;
                else
                    Destroy(_cropInstance);
            }
        }
    }

    private Vector3 CalculateCropPosition(Transform player)
    {
        var rayOrigin = player.position + new Vector3(0, 1, 0) + player.forward * plantingDistance;

        Debug.DrawRay(rayOrigin, Vector3.down * 10, Color.red, 0.05f, true);

        if (Physics.Raycast(rayOrigin, Vector3.down * 10, out RaycastHit hitInfo, 10)
            && hitInfo.collider is MeshCollider)
            return hitInfo.point;
        else
            return _cropInstance ? _cropInstance.transform.position : player.transform.position + player.forward * plantingDistance;
    }
}