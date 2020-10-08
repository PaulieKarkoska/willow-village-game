using UnityEngine;

public class FarmPlot : MonoBehaviour, IInteractable
{
    [Header("Farming")]
    [SerializeField]
    private float plantingDistance = 1.5f;
    [SerializeField]
    private GameObject _cropPrefab;
    private GameObject _cropInstance;

    private CollectableManager _playerInventory;
    private Camera _camera;

    private Quaternion _cropRotation = Quaternion.identity;

    public bool supportsIntermediateInteraction { get; } = true;

    public KeyCode interactionKey { get; } = KeyCode.E;

    void Start()
    {
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

    public string getInteractionText(GameObject player)
    {
        return "Plant crop";
    }

    public string getInteractionInvalidText(GameObject player)
    {
        if (_cropInstance)
        {
            var crop = _cropInstance.GetComponent<Crop>();
            switch (crop.state)
            {
                case CropState.Valid:
                case CropState.Invalid:
                    if (crop.hasCropCollision)
                        return "Cannot place crop so close to another crop";
                    else if (!crop.isInBounds)
                        return "Can only plant crop in center of farm plot";
                    else
                        return "Invalid crop placement";

                case CropState.Planted:
                case CropState.NeedsWater:
                    return "Not enough water in inventory";

                default:
                    return string.Empty;
            }
        }
        else
            return string.Empty;
    }

    public bool canInteract(GameObject player)
    {
        if (_cropInstance)
        {
            var crop = _cropInstance.GetComponent<Crop>();
            switch (crop.state)
            {
                case CropState.Valid:
                case CropState.Invalid:
                    return !crop.hasCropCollision && crop.isInBounds;

                case CropState.Planted:
                case CropState.NeedsWater:
                    return crop.playerInventory.HasEnoughWater(1);

                case CropState.Dead:
                case CropState.Complete:
                    return true;

                default:
                    return false;
            }
        }
        return _cropInstance;
    }

    public void interact(GameObject player)
    {
        _cropInstance.GetComponent<Crop>().Plant();
        _cropInstance = null;
    }

    public bool canIntermediateInteract(GameObject player)
    {
        return true;

    }

    public void intermediateInteract(GameObject player)
    {
        if (_cropInstance == null)
        {
            _cropInstance = Instantiate(_cropPrefab, player.GetComponent<InteractionManager>().lookPoint, _cropRotation);
            _cropInstance.GetComponent<Crop>().playerInventory = player.GetComponent<CollectableManager>();
        }

        _cropInstance.transform.position = player.GetComponent<InteractionManager>().lookPoint;
    }

    public void focusLost(GameObject obj)
    {
        if (_cropInstance)
        {
            Destroy(_cropInstance);
        }
    }
}