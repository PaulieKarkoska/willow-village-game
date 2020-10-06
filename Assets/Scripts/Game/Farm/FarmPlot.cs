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
    private Camera _camera;

    private Quaternion _cropRotation = Quaternion.identity;

    void Start()
    {
        if (!farmerCollider)
            Debug.Log("There is no farmer collider");

        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (_cropInstance && scroll != 0)
        {
            var rotation = (scroll > 0 ? Vector3.up : Vector3.down) * 3;
            _cropInstance.transform.Rotate(rotation, Space.Self);
            _cropRotation = _cropInstance.transform.rotation;
        }
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
                    _cropInstance = Instantiate(_cropPrefab, CalculateCropPosition(other.transform, out _), _cropRotation);
                    _cropInstance.GetComponent<Crop>().playerInventory = _playerInventory;
                }
                else
                {
                    _cropInstance.transform.position = CalculateCropPosition(other.transform, out _);
                }
            }
            else if (!_cropInstance && _playerInventory.HasEnoughSeeds(1))
            {
                var pos = CalculateCropPosition(other.transform, out bool isInRange);
                if (isInRange)
                {
                    _cropInstance = Instantiate(_cropPrefab, pos, _cropRotation);
                    _cropInstance.GetComponent<Crop>().playerInventory = _playerInventory;
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
                _cropInstance = Instantiate(_cropPrefab, CalculateCropPosition(other.transform, out bool isInRange), other.transform.rotation);
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

    private Vector3 CalculateCropPosition(Transform player, out bool isInRange)
    {
        //var rayOrigin = player.position + new Vector3(0, 1, 0) + player.forward * plantingDistance;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.05f, true);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo, 10)
            && hitInfo.collider is MeshCollider)
        {
            isInRange = true;
            return hitInfo.point;
        }
        else
        {
            isInRange = false;
            Destroy(_cropInstance);
            return _cropInstance ? _cropInstance.transform.position : player.transform.position + player.forward * plantingDistance;
        }
    }
}