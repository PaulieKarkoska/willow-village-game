using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour, IInteractable
{
    #region Properties and Fields
    [Header("Materials")]
    [SerializeField]
    Material validMaterial;
    [SerializeField]
    Material invalidMaterial;
    [SerializeField]
    Material defaultMaterial;
    [SerializeField]
    Material deadMaterial;

    [Header("Audio")]
    [SerializeField]
    private AudioClip waterClip;
    [SerializeField]
    private AudioClip[] harvestRemoveClips;

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
    private GameObject plantMeshGameObject;
    [SerializeField]
    private Vector3 originalPlantScale = new Vector3(0.15f, 0.15f, 0.15f);
    [SerializeField]
    private float timeToHarvest = 120f;
    private float remainingHarvestTime;
    [SerializeField]
    private float waterInterval = 60;
    private float remainingWaterTime;
    [SerializeField]
    private float maxRotTime = 180f;
    private float currentRotTime;

    [Header("Rewards")]
    [SerializeField]
    private static int minCoinReward = 1;
    [SerializeField]
    private static int maxCoinReward = 3;
    [Space(height: 10f)]
    [SerializeField]
    private static int minSeedReward = 1;
    [SerializeField]
    private static int maxSeedReward = 1;

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
    public CropState state
    {
        get { return _state; }
        private set
        {
            var lastState = _state;
            _state = value;
            OnStateChanged(lastState, value);
        }
    }

    public CollectableManager playerInventory { get; set; }

    public KeyCode interactionKey { get; } = KeyCode.E;

    public bool isInBounds { get; private set; } = false;
    public bool hasCropCollision { get; private set; } = false;
    #endregion

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _waterSlider = GetComponentInChildren<Slider>(true);
        plantMeshGameObject.transform.localScale = originalPlantScale;
        SetValidity();
    }

    private void Update()
    {
        switch (state)
        {
            case CropState.Planted:
                remainingHarvestTime -= Time.deltaTime;
                remainingWaterTime -= Time.deltaTime;
                if (remainingWaterTime <= 0f)
                    state = CropState.NeedsWater;
                else
                {
                    plantMeshGameObject.transform.localScale = Vector3.Lerp(originalPlantScale, Vector3.one, 1 - (remainingHarvestTime / timeToHarvest));

                    remainingHarvestTime -= Time.deltaTime;
                    if (remainingHarvestTime <= 0)
                        state = CropState.Complete;
                }
                break;

            case CropState.NeedsWater:
                currentRotTime -= Time.deltaTime;
                if (currentRotTime <= 0)
                    state = CropState.Dead;
                break;

            default:
                break;
        }
    }

    private void LateUpdate()
    {
        if (state == CropState.Planted)
        {
            _waterSlider.value = remainingWaterTime / waterInterval;
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
            state = isInBounds && !hasCropCollision && playerInventory.HasEnoughSeeds(1) ? CropState.Valid : CropState.Invalid;
    }

    public void Plant()
    {
        var plant = gameObject.transform.Find("Plant");
        plant.gameObject.SetActive(true);
        var thisMeshRenderer = GetComponent<MeshRenderer>();
        thisMeshRenderer.enabled = false;
        state = CropState.Planted;
    }

    public void Water()
    {
        AudioSource.PlayClipAtPoint(waterClip, transform.position);
        state = CropState.Planted;
    }

    public void Kill()
    {
        state = CropState.Dead;
    }

    public void Harvest()
    {
        PlayHarvestRemoveClip();
        var emitter = GetComponent<PrefabEmitter>();
        emitter.Emit(transform.position, Vector3.up * 200, new Vector3(0.1f, 0.1f, 0.1f), 0, minCoinReward, maxCoinReward);
        emitter.Emit(transform.position, Vector3.up * 200, new Vector3(0.1f, 0.1f, 0.1f), 1, minSeedReward, maxSeedReward);

        StartCoroutine(ShrinkAndDisappear());
    }

    public void Remove()
    {
        PlayHarvestRemoveClip();
        StartCoroutine(ShrinkAndDisappear());
    }

    private void OnStateChanged(CropState previous, CropState current)
    {
        switch (current)
        {
            case CropState.Invalid:
                OnIsInvalid();
                break;
            case CropState.Valid:
                OnIsValid();
                break;
            case CropState.Planted:
                OnWatered(previous == CropState.NeedsWater || previous == CropState.Planted);
                OnPlanted(previous == CropState.Valid);
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
            case CropState.Disappearing:
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
    private void OnPlanted(bool isFirstTime = true)
    {
        if (isFirstTime)
        {
            remainingHarvestTime = timeToHarvest;
            playerInventory.RemoveSeeds(1);
        }

        remainingWaterTime = waterInterval;

        _meshRenderer.material = defaultMaterial;
        gameObject.layer = 19;

    }
    private void OnWatered(bool takeWater = true)
    {
        if (takeWater)
            playerInventory.RemoveWater(1);

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
                _meshRenderer.material = deadMaterial;
                _waterSlider.gameObject.SetActive(false);
                plantMeshGameObject.GetComponent<MeshRenderer>().material = deadMaterial;
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

    public bool supportsIntermediateInteraction { get; } = false;

    public string getInteractionText(GameObject player)
    {
        switch (state)
        {
            case CropState.Planted:
            case CropState.NeedsWater:
                return "Water crop";

            case CropState.Complete:
                return "Harvest crop";

            case CropState.Dead:
                return "Remove dead crop";

            case CropState.Valid:
                return "Plant crop";

            default:
                return string.Empty;
        }
    }

    public string getInteractionInvalidText(GameObject player)
    {
        switch (state)
        {
            case CropState.Valid:
            case CropState.Invalid:
                return "Cannot place crop so close to another crop";

            case CropState.Planted:
            case CropState.NeedsWater:
                return "Not enough water in inventory";

            default:
            case CropState.Disappearing:
                return string.Empty;
        }
    }

    public bool canInteract(GameObject player)
    {
        switch (state)
        {
            case CropState.Planted:
            case CropState.NeedsWater:
                return playerInventory.HasEnoughWater(1);

            case CropState.Dead:
            case CropState.Complete:
                return true;

            default:
            case CropState.Disappearing:
                return false;
        }
    }

    public void interact(GameObject player)
    {
        switch (state)
        {
            case CropState.Planted:
            case CropState.NeedsWater:
                Water();
                return;

            case CropState.Complete:
                Harvest();
                return;

            case CropState.Dead:
                Remove();
                return;
        }
    }

    public bool canIntermediateInteract(GameObject player)
    {
        throw new NotImplementedException($"canIntermediateInteract is not supported and should not be called on this object: {this.GetType().Name}");
    }

    public void intermediateInteract(GameObject player)
    {
        throw new NotImplementedException($"intermediateInteract is not supported and should not be called on this object: {this.GetType().Name}");
    }

    public void focusLost(GameObject obj)
    {
    }

    private void PlayHarvestRemoveClip()
    {
        if (harvestRemoveClips?.Length > 0)
        {
            var index = UnityEngine.Random.Range(0, harvestRemoveClips.Length - 1);
            AudioSource.PlayClipAtPoint(harvestRemoveClips[index], transform.position);
        }
    }
    private IEnumerator ShrinkAndDisappear()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;

        var origScale = plantMeshGameObject.transform.localScale;
        var totalTime = 0.1f;

        var currentTime = 0f;

        do
        {
            plantMeshGameObject.transform.localScale = Vector3.Lerp(origScale, Vector3.zero, currentTime / totalTime);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= totalTime);

        //If harvesting and completed, explode with coins, leaves, and seeds
        Destroy(gameObject);
    }
}