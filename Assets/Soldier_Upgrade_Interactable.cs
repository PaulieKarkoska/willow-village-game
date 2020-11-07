using UnityEngine;

public class Soldier_Upgrade_Interactable : BuyableUpgrade, IInteractable
{
    [SerializeField]
    private int originalCostOverride = 45;
    [SerializeField]
    private int originalMaxLevelOverride = 3;
    private void Awake()
    {
        originalMoneyCost = originalCostOverride;
        currentMoneyCost = originalMoneyCost;
        maxLevel = originalMaxLevelOverride;
    }

    public bool supportsIntermediateInteraction { get; set; } = false;

    public KeyCode interactionKey { get; set; } = KeyCode.E;

    public override string upgradeName { get; set; } = "Soldier Armor Upgrade";

    public bool canInteract(GameObject player)
    {
        FocusCostBanner();

        return GetCanUpgrade() && player.GetComponent<CollectableManager>().HasEnoughMoney(currentMoneyCost);
    }

    public bool canIntermediateInteract(GameObject player)
    {
        return false;
    }

    public void focusLost(GameObject player)
    {
        UnfocusCostBanner();
    }

    public string getInteractionInvalidText(GameObject player)
    {
        if (!player.GetComponent<CollectableManager>().HasEnoughMoney(currentMoneyCost))
            return $"Not enough money to buy {upgradeName}";
        else if (!GetCanUpgrade())
            return "This upgrade is at max level";
        else
            return string.Empty;
    }

    public string getInteractionText(GameObject player)
    {
        return $"Buy level {currentLevel} {upgradeName}";
    }

    public void interact(GameObject player)
    {
        AllySpawner.UpgradeArmor();

        base.Buy(player.GetComponent<CollectableManager>());
    }

    public void intermediateInteract(GameObject player)
    {
    }
}
