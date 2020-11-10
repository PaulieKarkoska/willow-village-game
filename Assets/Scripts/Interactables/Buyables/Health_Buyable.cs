using Invector.vCharacterController;
using UnityEngine;

public class Health_Buyable : BuyableUpgrade, IInteractable
{
    [SerializeField]
    private int originalCostOverride = 20;
    [SerializeField]
    [Tooltip("Make negative for unlimited upgrades")]
    private int originalMaxLevelOverride = -1;
    private void Awake()
    {
        originalMoneyCost = originalCostOverride;
        currentMoneyCost = originalMoneyCost;
        maxLevel = originalMaxLevelOverride;
    }

    public bool supportsIntermediateInteraction { get; set; } = false;

    public KeyCode interactionKey { get; set; } = KeyCode.E;

    public override string upgradeName { get; set; } = "Restore Health Potion";

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
        return $"Not enough money to buy {upgradeName}";
    }

    public string getInteractionText(GameObject player)
    {
        return $"Buy {upgradeName} and restore health";
    }

    public void interact(GameObject player)
    {
        base.Buy(player.GetComponent<CollectableManager>());
        player.GetComponent<vThirdPersonController>().ResetHealth();
    }

    public void intermediateInteract(GameObject player)
    {
    }
}