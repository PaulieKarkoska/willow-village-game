using UnityEngine;

public class KeepChest : MonoBehaviour, IInteractable
{
    private CollectableManager _inventory;
    public static int totalChestMoney { get; private set; }
    public delegate void GoldUpdated(string goldText);
    public static event GoldUpdated OnGoldUpdated;

    public bool HasEnoughMoney(int cost)
    {
        return totalChestMoney >= cost;
    }

    public int RemoveMoney(int cost)
    {
        totalChestMoney -= cost;
        OnGoldUpdated(totalChestMoney.ToString("#,##0"));
        return totalChestMoney;
    }

    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectableManager>();
    }

    public bool supportsIntermediateInteraction => false;

    public KeyCode interactionKey => KeyCode.E;

    public bool canInteract(GameObject player)
    {
        return _inventory.HasEnoughMoney(1);
    }

    public bool canIntermediateInteract(GameObject player)
    {
        return false;
    }

    public void focusLost(GameObject player)
    {
    }

    public string getInteractionInvalidText(GameObject player)
    {
        return $"No coins to submit to the keep";
    }

    public string getInteractionText(GameObject player)
    {

        return $"Submit {_inventory.totalMoney} coins to the keep";
    }

    public void interact(GameObject player)
    {
        var add = _inventory.totalMoney;
        totalChestMoney += add;
        _inventory.RemoveMoney(_inventory.totalMoney);

        OnGoldUpdated?.Invoke(totalChestMoney.ToString("#,##0"));
    }

    public void intermediateInteract(GameObject player)
    {
    }
}