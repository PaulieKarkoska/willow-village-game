using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField]
    Material validMaterial;
    [SerializeField]
    Material invalidMaterial;
    [SerializeField]
    Material defaultMaterial;
    [SerializeField]
    Material deadMaterial;

    [Header("GUI")]
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private Sprite waterIcon;
    [SerializeField]
    private Sprite deadIcon;
    [SerializeField]
    private Sprite harvestIcon;

    [Header("Farming")]
    [SerializeField]
    private float timeToHarvest = 120f;
    private float remainingHarvestTime;
    [SerializeField]
    private float waterInterval = 60;
    private float remainingWaterTime;
    [SerializeField]
    private float maxRotTime = 180f;
    private float currentRotTime;

    public bool isPlanted
    {
        get
        {
            return _state == CropState.Planted
                || _state == CropState.NeedsWater
                || _state == CropState.Dead
                || _state == CropState.Complete;
        }
    }

    private MeshRenderer _meshRenderer;
    private Slider _waterSlider;

    private CropState _state = CropState.Invalid;
    private CropState state
    {
        get { return _state; }
        set
        {
            _state = value;
            OnStateChanged(value);
        }
    }

    public CollectableManager playerInventory { get; set; }

    private bool isInBounds = false;
    private bool hasCropCollision = false;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _waterSlider = GetComponentInChildren<Slider>(true);
        iconImage.sprite = deadIcon;
        SetValidity();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            switch (state)
            {
                case CropState.Valid:
                    Plant();
                    break;
                case CropState.Dead:
                    Remove();
                    break;
                case CropState.Complete:
                    Harvest();
                    break;
            }
    }

    private void FixedUpdate()
    {
        if (isPlanted)
        {
            timeToHarvest -= Time.fixedDeltaTime;
            remainingWaterTime -= Time.fixedDeltaTime;
            if (remainingWaterTime <= 0f)
                state = CropState.NeedsWater;
            else
            {
                remainingHarvestTime -= Time.fixedDeltaTime;
                if (remainingHarvestTime <= 0)
                    state = CropState.Complete;
            }
        }
    }

    private void LateUpdate()
    {
        switch (state)
        {
            case CropState.Planted:
                _waterSlider.value = remainingWaterTime / waterInterval;
                break;

            case CropState.NeedsWater:
                currentRotTime -= Time.deltaTime;
                if (currentRotTime <= 0)
                    Kill();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropBounds"))
        {
            isInBounds = true;
            SetValidity();
        }
        else if (other.CompareTag("Crop"))
        {
            hasCropCollision = true;
            SetValidity();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crop"))
        {
            hasCropCollision = true;
            SetValidity();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropBounds"))
        {
            isInBounds = false;
            SetValidity();
        }
        else if (other.CompareTag("Crop"))
        {
            hasCropCollision = false;
            SetValidity();
        }
    }

    private void SetValidity()
    {
        if (!isPlanted)
            state = isInBounds && !hasCropCollision ? CropState.Valid : CropState.Invalid;
    }

    public void Plant()
    {
        playerInventory.RemoveSeeds(1);
        state = CropState.Planted;
    }

    public void Water()
    {
        OnWatered();
    }

    public void Kill()
    {
        state = CropState.Dead;
    }

    public void Harvest()
    {

    }

    public void Remove()
    {
    }

    private void OnStateChanged(CropState value)
    {
        switch (value)
        {
            case CropState.Invalid:
                OnIsInvalid();
                break;
            case CropState.Valid:
                OnIsValid();
                break;
            case CropState.Planted:
                OnPlanted();
                OnWatered();
                break;
            case CropState.NeedsWater:
                OnNeedsWater();
                break;
            case CropState.Dead:
                OnDead();
                break;
            case CropState.Complete:
                OnCompleted();
                break;
        }
        UpdateUi();
    }

    private void OnIsInvalid()
    {
        _meshRenderer.material = invalidMaterial;
    }
    private void OnIsValid()
    {
        _meshRenderer.material = validMaterial;
    }
    private void OnPlanted()
    {
        remainingHarvestTime = timeToHarvest;
        remainingWaterTime = waterInterval;

        _meshRenderer.material = defaultMaterial;
    }
    private void OnWatered()
    {
        remainingWaterTime = waterInterval;
    }
    private void OnNeedsWater()
    {
        currentRotTime = maxRotTime;
    }
    private void OnDead()
    {
    }
    private void OnCompleted()
    {
    }

    private void UpdateUi()
    {
        switch (state)
        {
            case CropState.Planted:
                iconImage.enabled = false;
                iconImage.sprite = null;
                _waterSlider.gameObject.SetActive(true);
                break;

            case CropState.NeedsWater:
                iconImage.enabled = true;
                iconImage.sprite = waterIcon;
                _waterSlider.gameObject.SetActive(false);
                break;

            case CropState.Dead:
                iconImage.enabled = true;
                iconImage.sprite = deadIcon;
                _waterSlider.gameObject.SetActive(false);
                break;

            case CropState.Complete:
                iconImage.enabled = true;
                iconImage.sprite = harvestIcon;
                _waterSlider.gameObject.SetActive(false);
                break;

                //default:
                //    iconImage.enabled = false;
                //    _waterSlider.gameObject.SetActive(false);
                //    break;

        }
    }
}

//private bool _isInBounds;
//private bool _isClearOfOtherCrops = true;
//public bool canPlant
//{
//    get { return GetCanPlant(); }
//}
//private bool _isReady = false;

//public bool isPlanted { get; private set; } = false;
//public bool isDead { get; private set; } = false;
//public bool needsWater { get; private set; } = false;

//void Start()
//{
//    _meshRenderer = GetComponent<MeshRenderer>();
//    _waterSlider = GetComponentInChildren<Slider>(true);
//    _cropUi = transform.GetChild(0).gameObject;
//    _isReady = true;
//}

//private void FixedUpdate()
//{
//    if (isPlanted && !needsWater)
//    {
//        timeToHarvest -= Time.fixedDeltaTime / 2;
//        remainingWaterTime -= Time.fixedDeltaTime / 2;
//        if (remainingWaterTime <= 0f)
//            needsWater = true;
//    }
//}

//private void Update()
//{
//    if (canPlant && Input.GetKeyDown(KeyCode.E))
//    {
//        Plant();
//    }
//}

//private void LateUpdate()
//{
//    if (isPlanted)
//    {
//        if (!needsWater)
//        {
//            UpdateWaterSlider();
//        }
//        else
//        {
//            currentRotTime += Time.deltaTime;
//            if (currentRotTime >= maxRotTime)
//                KillCrop();
//        }
//    }
//}

//private void OnTriggerEnter(Collider other)
//{
//    if (other.CompareTag("CropBounds"))
//    {
//        _isInBounds = true;
//        GetCanPlant();
//    }
//    else if (other.CompareTag("Crop"))
//    {
//        _isClearOfOtherCrops = false;
//        GetCanPlant();
//    }
//}

//private void OnTriggerExit(Collider other)
//{
//    if (other.CompareTag("CropBounds"))
//    {
//        _isInBounds = false;
//        GetCanPlant();
//    }
//    else if (other.CompareTag("Crop"))
//    {
//        _isClearOfOtherCrops = true;
//        GetCanPlant();
//    }
//}

//private void OnTriggerStay(Collider other)
//{
//    if (other.CompareTag("Crop"))
//    {
//        _isClearOfOtherCrops = false;
//        GetCanPlant();
//    }
//}

//private bool GetCanPlant(bool update = true)
//{
//    var result = !isPlanted && !isDead && _isInBounds && _isClearOfOtherCrops;
//    if (_isReady && update)
//        _meshRenderer.material = result ? validMaterial : invalidMaterial;

//    return result;
//}
//private void UpdateWaterSlider()
//{
//    _waterSlider.value = remainingWaterTime / waterInterval;
//}

//public void Plant()
//{
//    remainingHarvestTime = timeToHarvest;
//    remainingWaterTime = waterInterval;

//    GetComponent<SphereCollider>().enabled = false;
//    _cropUi.SetActive(true);

//    _meshRenderer.material = defaultMaterial;
//    isPlanted = true;
//}

//public void Water()
//{
//    needsWater = false;

//}

//public void KillCrop()
//{
//    isDead = true;
//    _meshRenderer.material = deadMaterial;
//}

//public void Harvest()
//{

//}
//}