using Invector.vMelee;
using UnityEngine;

public class Sword_Buyable : MonoBehaviour, IInteractable
{
    public static bool swordIsPurchased = false;
    [SerializeField]
    private int cost = 50;
    [SerializeField]
    private GameObject equippableSwordPrefab;

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
        return "Not enough money to buy sword";
    }

    public string getInteractionText(GameObject player)
    {
        return "Buy sword";
    }

    public void interact(GameObject player)
    {
        swordIsPurchased = true;
        player.GetComponent<CollectableManager>().RemoveMoney(cost);
        var eqp = player.GetComponent<PlayerEquipment>();
        eqp.weapon.GetComponent<vMeleeWeapon>().enabled = true;
        eqp.weapon.SetActive(true);
        eqp.meleeManager.SetRightWeapon(eqp.weapon);

        Destroy(gameObject);
    }

    public void intermediateInteract(GameObject player)
    {
    }
}