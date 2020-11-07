using Invector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealer : MonoBehaviour, IInteractable
{
    private CollectableManager inventory;
    private TreeOfLife tree;
    private vHealthController health;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectableManager>();
        tree = GameObject.Find("Tree of Life").GetComponent<TreeOfLife>();
        health = tree.gameObject.GetComponent<vHealthController>();
    }

    public bool supportsIntermediateInteraction => false;

    public KeyCode interactionKey => KeyCode.E;

    public bool canInteract(GameObject player)
    {
        return inventory.HasEnoughSeeds(1)
                && health.currentHealth < health.maxHealth;
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
        return "Cannot heal tree right now";
    }

    public string getInteractionText(GameObject player)
    {
        return "Spend seeds to restore tree health";
    }

    public void interact(GameObject player)
    {
        var missing = Mathf.CeilToInt(health.maxHealth - health.currentHealth);
        var healValue = missing < inventory.totalSeeds ? missing : inventory.totalSeeds;
        health.AddHealth(healValue);
        inventory.RemoveSeeds(healValue);
    }

    public void intermediateInteract(GameObject player)
    {
        throw new System.NotImplementedException();
    }
}
