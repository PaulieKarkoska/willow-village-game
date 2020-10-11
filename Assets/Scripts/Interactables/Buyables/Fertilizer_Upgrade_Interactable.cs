using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fertilizer_Upgrade_Interactable : BuyableUpgrade, IInteractable
{
    private void Awake()
    {
        originalMoneyCost = 55;
        currentMoneyCost = originalMoneyCost;
    }

    public bool supportsIntermediateInteraction { get; set; } = false;

    public KeyCode interactionKey { get; set; } = KeyCode.E;

    public override string upgradeName { get; set; } = "Harvest Reward Upgrade";

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
        else if (GetCanUpgrade())
            return "This upgrade is at max level";
        else
            return string.Empty;
    }

    public string getInteractionText(GameObject player)
    {
        return $"Buy level {currentLevel + 1} {upgradeName}";
    }

    public void interact(GameObject player)
    {
        Crop.UpgradeHarvestReward();

        base.Buy(player.GetComponent<CollectableManager>());
    }

    public void intermediateInteract(GameObject player)
    {
    }
}
