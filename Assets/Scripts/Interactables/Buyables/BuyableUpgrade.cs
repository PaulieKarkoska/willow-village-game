using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuyableUpgrade : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private AudioClip[] audioClips;
    private Image costBanner;

    private void Start()
    {
        UpdateCostText();
        costBanner = transform.Find("CostBanner").GetComponent<Image>();
    }

    public abstract string upgradeName { get; set; }
    public int maxLevel { get; set; } = 20;
    public int currentLevel { get; set; } = 1;

    public int originalMoneyCost { get; set; }
    public int currentMoneyCost { get; set; }

    public virtual int SetNewMoneyCost()
    {
        currentMoneyCost = (int)(currentMoneyCost + (originalMoneyCost * (0.1f + (currentLevel * 0.1f))));

        return currentMoneyCost;
    }

    public bool GetCanUpgrade()
    {
        return currentLevel <= maxLevel || maxLevel < 0;
    }

    public void Buy(CollectableManager playerInventory)
    {
        currentLevel++;

        playerInventory.RemoveMoney(currentMoneyCost);
        SetNewMoneyCost();

        UpdateCostText();

        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length - 1)], transform.position);
    }

    private void UpdateCostText()
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().text = GetCanUpgrade() ? $"${currentMoneyCost}" : "Max";
    }

    protected void FocusCostBanner()
    {
        var tempColor = costBanner.color;
        tempColor.a = 1f;
        costBanner.color = tempColor;
    }

    protected void UnfocusCostBanner()
    {
        var tempColor = costBanner.color;
        tempColor.a = 0.3f;
        costBanner.color = tempColor;
    }
}