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

    [Header("Farming")]
    [SerializeField]
    private float timeToHarvest = 120f;
    private float remainingHarvestTime;
    [SerializeField]
    private float waterInterval = 60f;
    private float remainingWaterTime;
    [SerializeField]
    private float maxRotTime = 180f;
    private float currentRotTime;

    private MeshRenderer _meshRenderer;
    private Slider _waterSlider;
    private GameObject _cropUi;

    private bool _isInBounds;
    private bool _isClearOfOtherCrops = true;
    public bool canPlant
    {
        get { return GetCanPlant(); }
    }
    private bool _isReady = false;

    public bool isPlanted { get; private set; } = false;
    public bool isDead { get; private set; } = false;
    public bool needsWater { get; private set; } = false;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _waterSlider = GetComponentInChildren<Slider>(true);
        _cropUi = transform.GetChild(0).gameObject;
        _isReady = true;
    }

    private void FixedUpdate()
    {
        if (isPlanted && !needsWater)
        {
            timeToHarvest -= Time.fixedDeltaTime / 2;
            remainingWaterTime -= Time.fixedDeltaTime / 2;
            if (remainingWaterTime <= 0f)
                needsWater = true;
        }
    }

    private void Update()
    {
        if (canPlant && Input.GetKeyDown(KeyCode.E))
        {
            Plant();
        }
    }

    private void LateUpdate()
    {
        if (isPlanted)
        {
            if (!needsWater)
            {
                UpdateWaterSlider();
            }
            else
            {
                currentRotTime += Time.deltaTime;
                if (currentRotTime >= maxRotTime)
                    KillCrop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropBounds"))
        {
            _isInBounds = true;
            GetCanPlant();
        }
        else if (other.CompareTag("Crop"))
        {
            _isClearOfOtherCrops = false;
            GetCanPlant();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropBounds"))
        {
            _isInBounds = false;
            GetCanPlant();
        }
        else if (other.CompareTag("Crop"))
        {
            _isClearOfOtherCrops = true;
            GetCanPlant();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crop"))
        {
            _isClearOfOtherCrops = false;
            GetCanPlant();
        }
    }

    private bool GetCanPlant(bool update = true)
    {
        var result = !isPlanted && !isDead && _isInBounds && _isClearOfOtherCrops;
        if (_isReady && update)
            _meshRenderer.material = result ? validMaterial : invalidMaterial;

        return result;
    }
    private void UpdateWaterSlider()
    {
        _waterSlider.value = remainingWaterTime / waterInterval;
    }

    public void Plant()
    {
        remainingHarvestTime = timeToHarvest;
        remainingWaterTime = waterInterval;

        GetComponent<SphereCollider>().enabled = false;
        _cropUi.SetActive(true);

        _meshRenderer.material = defaultMaterial;
        isPlanted = true;
    }

    public void Water()
    {
        needsWater = false;
        
    }

    public void KillCrop()
    {
        isDead = true;
        _meshRenderer.material = deadMaterial;
    }

    public void Harvest()
    {

    }
}