using Invector.vMelee;
using UnityEngine;

public class Shield_Buyable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int cost = 50;
    [SerializeField]
    private GameObject equippableShieldPrefab;

    public bool supportsIntermediateInteraction => false;

    public KeyCode interactionKey => KeyCode.E;

    public bool canInteract(GameObject player)
    {
        return player.GetComponent<CollectableManager>().HasEnoughMoney(cost);
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
        return "Not enough money to buy shield";
    }

    public string getInteractionText(GameObject player)
    {
        return "Buy shield";
    }

    public void interact(GameObject player)
    {
        player.GetComponent<CollectableManager>().RemoveMoney(cost);
        var leftHandler = player.GetComponent<vCollectMeleeControl>().leftHandler.defaultHandler;
        var shield = Instantiate(equippableShieldPrefab, leftHandler, false);
        player.GetComponent<vMeleeManager>().SetRightWeapon(shield);
        Destroy(gameObject);
    }

    public void intermediateInteract(GameObject player)
    {
    }
}