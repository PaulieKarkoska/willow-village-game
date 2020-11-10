using Invector;
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
        if (health.currentHealth >= health.maxHealth)
            return "Tree health is full";
        else if (!inventory.HasEnoughSeeds(1))
            return "You need seeds to heal the tree";
        else
            return "Cannot heal tree right now";
    }

    public string getInteractionText(GameObject player)
    {
        return $"Spend {health} seeds to restore tree health";
    }

    public void interact(GameObject player)
    {
        var needed = Mathf.CeilToInt((health.maxHealth - health.currentHealth) / 2);
        needed = inventory.totalSeeds >= needed ? needed : inventory.totalSeeds;
        health.ResetHealth();
        inventory.RemoveSeeds(needed);
    }

    public void intermediateInteract(GameObject player)
    {
        throw new System.NotImplementedException();
    }
}
