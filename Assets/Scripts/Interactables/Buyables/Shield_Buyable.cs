using Invector.vMelee;
using UnityEngine;

public class Shield_Buyable : MonoBehaviour, IInteractable
{
    public static bool shieldIsPurchased = false;

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
        shieldIsPurchased = true;
        player.GetComponent<CollectableManager>().RemoveMoney(cost);
        var eqp = player.GetComponent<PlayerEquipment>();
        eqp.shield.GetComponent<vMeleeWeapon>().enabled = true;
        eqp.shield.SetActive(true);
        eqp.meleeManager.SetRightWeapon(eqp.weapon);

        Destroy(gameObject);
    }

    public void intermediateInteract(GameObject player)
    {
    }
}