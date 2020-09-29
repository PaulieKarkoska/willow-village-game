using UnityEngine;

public class FarmPlot : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField]
    private Collider farmerCollider;
    private bool playerIsInBounds;

    [Header("Farming")]
    [SerializeField]
    private GameObject _cropPrefab;
    private GameObject _cropInstance;

    void Start()
    {
        if (!farmerCollider)
            Debug.Log("There is no farmer collider");
    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player is staying in the farm plot");

            if (_cropInstance)
            {
                if (_cropInstance.GetComponent<Crop>().isPlanted)
                {
                    _cropInstance = null;
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
            Debug.Log("player entered farm plot");
            playerIsInBounds = true;

            if (!_cropInstance)
                _cropInstance = Instantiate(_cropPrefab, CalculateCropPosition(other.transform), other.transform.rotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player exited farm plot");
            playerIsInBounds = false;
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
        var playerPos = player.position;
        var playerDir = player.forward;
        var spawnDist = 2f;

        return playerPos + playerDir * spawnDist;
    }
}